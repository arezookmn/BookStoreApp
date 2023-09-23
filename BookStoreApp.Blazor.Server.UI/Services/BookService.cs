using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
    public class BookService :  BaseHttpService, IBookService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorage;

        public BookService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        public async Task<Response<int>> CreateBook(BookCreateDTO bookCreateDto)
        {
            Response<int> response = new Response<int>() { Success = true };
            try
            {
                await GetBearerToken();
                await _client.BooksPOSTAsync(bookCreateDto);
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<int>(exception: ex);
            }

            return response;
        }

        public async Task<Response<List<BookResponseDTO>>> GetBooks()
        {
            var response = new Response<List<BookResponseDTO>>();

            try
            {
                await GetBearerToken();
                var data = await _client.BooksAllAsync();
                response = new Response<List<BookResponseDTO>>()
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertApiException<List<BookResponseDTO>>(exception: apiException);

            }
            return response;
        }

        public async Task<Response<int>> EditBook(int id, BookUpdateDTO bookUpdateDto)
        {
            Response<int> response = new Response<int>() { Success = true };
            try
            {
                await GetBearerToken();
                await _client.BooksPUTAsync(id, bookUpdateDto);
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<int>(exception: ex);
            }

            return response;
        }

        public async Task<Response<BookResponseDTO>> GetBookById(int id)
        {
            var response = new Response<BookResponseDTO>();

            try
            {
                await GetBearerToken();
                var data = await _client.BooksGETAsync(id);
                response = new Response<BookResponseDTO>()
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertApiException<BookResponseDTO>(exception: apiException);

            }
            return response;
        }

        public async Task<Response<BookUpdateDTO>> GetBookForUpdate(int id)
        {
            var response = new Response<BookUpdateDTO>();

            try
            {
                await GetBearerToken();
                var data = await _client.BooksGETAsync(id);
                response = new Response<BookUpdateDTO>()
                {
                    Data = new BookUpdateDTO()
                    {
                        Id = data.Id,
                        AuthorId = data.AuthorId,
                        Image = data.Image,
                        Isbn = data.Isbn,
                        Price = data.Price,
                        Summery = data.Summery,
                        Title = data.Title
                    },
                    Success = true
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertApiException<BookUpdateDTO>(exception: apiException);

            }
            return response;
        }

        public async Task<Response<int>> DeleteBook(int bookId)
        {
            var response = new Response<int>();
            try
            {
                await GetBearerToken();
                await _client.BooksDELETEAsync(bookId);

            }
            catch (ApiException ex)
            {
                response = ConvertApiException<int>(exception: ex);
            }
            return response;
        }
    }
}

