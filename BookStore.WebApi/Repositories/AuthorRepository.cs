using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.WebApi.Controllers;
using BookStore.WebApi.Data;
using BookStore.WebApi.DTOModels.AuthorDTO;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebApi.Repositories
{
    public class AuthorRepository : GenericRepository<Author> , IAuthorsRepository
    {
        private readonly BookStoreAppDbContext _dbContext;
        private readonly IMapper _mapper;
        public AuthorRepository( BookStoreAppDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AuthorDetailsDTO> GetAuthorDetailsAsync(int id)
        {
            var author =  await _dbContext.Authors
                .Include(a => a.Books)
                .ProjectTo<AuthorDetailsDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == id);
            return author;
        }
    }
}
