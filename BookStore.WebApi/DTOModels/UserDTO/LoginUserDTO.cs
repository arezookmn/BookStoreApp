using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApi.DTOModels.UserDTO
{
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
