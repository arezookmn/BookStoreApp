using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.WebApi.Configuration;
using BookStore.WebApi.DTOModels.BookDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BookStore.WebApi.Data;
using BookStore.WebApi.Repositories;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(IBookRepository bookRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResponseDTO>>> GetBooks()
        {

            IEnumerable<BookResponseDTO> bookResponses = await _bookRepository.GetAllBookResponse();

            return Ok(bookResponses);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookResponseDTO>> GetBook(int id)
        {
            var book = await _bookRepository.GetBookResponse(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDTO bookUpdateDto)
        {

            if (id != bookUpdateDto.Id)
            {
                return BadRequest();
            }
            Book book = _mapper.Map<Book>(bookUpdateDto);
            if (string.IsNullOrEmpty(bookUpdateDto.ImageData) == false)
            {
                book.Image = CreateFile(bookUpdateDto.ImageData, bookUpdateDto.OriginalImageName);
            }

            try
            {
                await _bookRepository.UpdateAsync(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bookRepository.Exist(id))
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

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookCreateDTO bookDto)
        {
            Book book = _mapper.Map<Book>(bookDto);
            book.Image = CreateFile(bookDto.ImageData, bookDto.OriginalImageName);

           await _bookRepository.AddAsync(book);

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        private string CreateFile(string? imageBase64, string? imageName)
        {
            if(imageBase64 == null) return string.Empty;

            var url = HttpContext.Request.Host.Value;
            var extension = Path.GetExtension(imageName);
            var fileName = Guid.NewGuid().ToString()+extension;
            var path = $"{_webHostEnvironment.WebRootPath}\\BookCoverImages\\{fileName}";

            byte[] image = Convert.FromBase64String(imageBase64);
            var fileStream = System.IO.File.Create(path);
            fileStream.Write(image,0, image.Length);
            fileStream.Close();

            return $"https://{url}/BookCoverImages/{fileName}";
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            await _bookRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> BookExists(int id)
        {
            return await _bookRepository.Exist(id);
        }
    }
}
