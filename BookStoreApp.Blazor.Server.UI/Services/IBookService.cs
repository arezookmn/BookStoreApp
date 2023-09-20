using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
    public interface IBookService
    {
        Task<Response<List<BookResponseDTO>>> GetBooks();
        Task<Response<int>> CreateBook(BookCreateDTO book);
        Task<Response<int>> EditAuthor(int id, AuthorUpdateDTO authorUpdateDto);
        Task<Response<AuthorDetailsDTO>> GetAuthorById(int id);
        Task<Response<AuthorUpdateDTO>> GetAuthorForUpdate(int id);
        Task<Response<int>> DeleteAuthor(int authorId);
    }
}
