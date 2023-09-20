using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using BookStore.WebApi.Data;
using BookStore.WebApi.DTOModels.UserDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;    
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(ILogger<AccountController> logger, IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDto)
        {
            try
            {
                var user = _mapper.Map<ApiUser>(userRegisterDto);
                user.UserName = userRegisterDto.Email;

                var result = await _userManager.CreateAsync(user, userRegisterDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);

                }
                await _userManager.AddToRoleAsync(user, "User");

                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,$"Something went wrong{nameof(Register)}");
                return Problem($"Something went wrong{nameof(Register)}", statusCode:500);
            }

        }



        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponse>> Login(LoginUserDTO userLoginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
                var passwordValid =await _userManager.CheckPasswordAsync(user ,userLoginDto.Password);

                if (user == null && passwordValid == false)
                {
                    return Unauthorized();
                }

                string tokenString = await GenerateToken(user);
                AuthResponse authResponse = new AuthResponse()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Token = tokenString
                };

                return Ok(authResponse);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong{nameof(Login)}");
                return Problem($"Something went wrong{nameof(Login)}", statusCode: 500);
            }
        }

        private async Task<string> GenerateToken(ApiUser user)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            SigningCredentials signinCridentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);
            var  roleClaims = roles.Select(t => new Claim(ClaimTypes.Role, t)).ToList();

            var userClaim = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),

            }
                .Union(userClaim)
                .Union(roleClaims);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTE"])),
                signingCredentials: signinCridentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
