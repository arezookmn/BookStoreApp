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
using Microsoft.AspNetCore.Authorization;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreAppDbContext _context;
        private readonly ILogger<AuthorsController> _logger;
        private readonly IMapper _mapper;

        public AuthorsController(BookStoreAppDbContext context, IMapper mapper,ILogger<AuthorsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorResponseDTO>>> GetAuthors()
        {
            try
            {
                if (_context.Authors == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<IEnumerable<AuthorResponseDTO>>(await _context.Authors.ToListAsync()));
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
                if (_context.Authors == null)
                {
                    return NotFound();
                }

                var author = await _context.Authors
                    .Include(a => a.Books)
                    .ProjectTo<AuthorDetailsDTO>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (author == null)
                {
                    _logger.LogWarning($"Record Not Found {nameof(GetAuthor)}");
                    return NotFound();
                }

                return author;
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
            Author author = _mapper.Map<Author>(updateAuthor);

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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
                if (_context.Authors == null)
                {
                    return Problem("Entity set 'BookStoreAppDbContext.Authors'  is null.");
                }

                Author author = _mapper.Map<Author>(authorDto);
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();

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
                if (_context.Authors == null)
                {
                    return NotFound();
                }

                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    return NotFound();
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing delete {nameof(DeleteAuthor)}");
                return Problem(ex.Message, statusCode: 500);
            }

        }

        private bool AuthorExists(int id)
        {
            return (_context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
