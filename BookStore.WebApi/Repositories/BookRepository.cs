using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.WebApi.Data;
using BookStore.WebApi.DTOModels.BookDTO;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebApi.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly BookStoreAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreAppDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BookResponseDTO>> GetAllBookResponse()
        {
            return await _dbContext.Books
                .Include(t => t.Author)
                .ProjectTo<BookResponseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<BookResponseDTO> GetBookResponse(int id)
        {
            return await _dbContext.Books
                .Include(t => t.Author)
                .ProjectTo<BookResponseDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
