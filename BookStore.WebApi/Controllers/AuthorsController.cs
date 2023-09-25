using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.WebApi.DTOModels.AuthorDTO;
using BookStore.WebApi.Data;
using BookStore.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsRepository _authorsRepository;
        private readonly ILogger<AuthorsController> _logger;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorsRepository authorsRepository, IMapper mapper,ILogger<AuthorsController> logger)
        {
            _authorsRepository = authorsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorResponseDTO>>> GetAuthors()
        {
            try
            {
               
                return Ok(_mapper.Map<IEnumerable<AuthorResponseDTO>>(await _authorsRepository.GetAllAsync()));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,$"Error performing Get {nameof(GetAuthors)}");
                return BadRequest(ex.Message);
            }

        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDetailsDTO>> GetAuthor(int id)
        {

            try
            {
                var author = _authorsRepository.GetAuthorDetailsAsync(id);

                if (author == null)
                {
                    _logger.LogWarning($"Record Not Found {nameof(GetAuthor)}");
                    return NotFound();
                }

                return Ok(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing Get {nameof(GetAuthor)}");
                return Problem(ex.Message, statusCode:500);
            }

        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDTO updateAuthor)
        {
            if (id != updateAuthor.Id)
            {
                return BadRequest();
            }

            Author? author = await _authorsRepository.GetAsync(id);
            try
            {
                await _authorsRepository.UpdateAsync(author);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _authorsRepository.Exist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorCreateDTO authorDto)
        {
            try
            {
                Author author = _mapper.Map<Author>(authorDto);
                await _authorsRepository.AddAsync(author);

                return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error performing post {nameof(PostAuthor)}", authorDto);
                return Problem(ex.Message, statusCode: 500);
            }
;
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {

                var author = await _authorsRepository.GetAsync(id);
                if (author == null)
                {
                    return NotFound();
                }

                await _authorsRepository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing delete {nameof(DeleteAuthor)}");
                return Problem(ex.Message, statusCode: 500);
            }

        }

        private async Task<bool> AuthorExists(int id)
        {
            return await _authorsRepository.Exist(id);
        }
    }
}
