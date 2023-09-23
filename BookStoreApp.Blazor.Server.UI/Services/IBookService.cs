using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
    public interface IBookService
    {
        Task<Response<List<BookResponseDTO>>> GetBooks();
        Task<Response<int>> CreateBook(BookCreateDTO book);
        Task<Response<int>> EditBook(int id, BookUpdateDTO bookUpdateDto);
        Task<Response<BookResponseDTO>> GetBookById(int id);
        Task<Response<BookUpdateDTO>> GetBookForUpdate(int id);
        Task<Response<int>> DeleteBook(int bookId);
    }
}
