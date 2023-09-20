using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorResponseDTO>>> GetAuthors();
        Task<Response<int>> CreateAuthor(AuthorCreateDTO authorCreateDto);
        Task<Response<int>> EditAuthor(int id, AuthorUpdateDTO authorUpdateDto);
        Task<Response<AuthorDetailsDTO>> GetAuthorById(int id);
        Task<Response<AuthorUpdateDTO>> GetAuthorForUpdate(int id);
        Task<Response<int>> DeleteAuthor(int authorId);
    }
}
