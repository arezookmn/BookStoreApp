using BookStore.WebApi.Data;
using BookStore.WebApi.DTOModels.BookDTO;

namespace BookStore.WebApi.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<BookResponseDTO>> GetAllBookResponse();
        Task<BookResponseDTO> GetBookResponse(int id);
    }
}
