using BookStore.WebApi.Data;
using BookStore.WebApi.DTOModels.AuthorDTO;

namespace BookStore.WebApi.Repositories
{
    public interface IAuthorsRepository : IGenericRepository<Author>
    {
        Task<AuthorDetailsDTO> GetAuthorDetailsAsync(int id);
    }
}
