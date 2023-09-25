using Blazored.LocalStorage;
using BookStoreApp.Blazor.WebAssembly.UI.Services;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services
{
    public class AuthorService : BaseHttpService , IAuthorService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorage;

        public AuthorService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        public async Task<Response<int>> CreateAuthor(AuthorCreateDTO authorCreateDto)
        {
            Response<int> response = new Response<int>(){Success = true};
            try
            {
                await GetBearerToken(); 
                await _client.AuthorsPOSTAsync(authorCreateDto);
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<int>(exception: ex);
            }

            return response;
        }

        public async Task<Response<List<AuthorResponseDTO>>> GetAuthors()
        {
            var response = new Response<List<AuthorResponseDTO>>();

            try
            {
                await GetBearerToken();
                var data = await _client.AuthorsAllAsync();
                response = new Response<List<AuthorResponseDTO>>()
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertApiException<List<AuthorResponseDTO>>(exception: apiException);

            }
            return response;
        }

        public async Task<Response<int>> EditAuthor(int id,AuthorUpdateDTO authorUpdateDto)
        {
            Response<int> response = new Response<int>() { Success = true };
            try
            {
                await GetBearerToken();
                await _client.AuthorsPUTAsync(id, authorUpdateDto);
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<int>(exception: ex);
            }

            return response;
        }

        public async Task<Response<AuthorDetailsDTO>> GetAuthorById(int id)
        {
            var response = new Response<AuthorDetailsDTO>();

            try
            {
                await GetBearerToken();
                var data = await _client.AuthorsGETAsync(id);
                response = new Response<AuthorDetailsDTO>()
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertApiException<AuthorDetailsDTO>(exception: apiException);

            }
            return response;
        }

        public async Task<Response<AuthorUpdateDTO>> GetAuthorForUpdate(int id)
        {
            var response = new Response<AuthorUpdateDTO>();

            try
            {
                await GetBearerToken();
                var data = await _client.AuthorsGETAsync(id);
                response = new Response<AuthorUpdateDTO>()
                {
                    Data = new AuthorUpdateDTO()
                    {
                        Bio = data.Bio,
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        Id = data.Id
                    },
                    Success = true
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertApiException<AuthorUpdateDTO>(exception: apiException);

            }
            return response;
        }

        public async Task<Response<int>> DeleteAuthor(int authorId)
        {
            var response = new Response<int>();
            try
            {
                await GetBearerToken();
                await _client.AuthorsDELETEAsync(authorId);

            }
            catch (ApiException ex)
            {
                response = ConvertApiException<int>(exception: ex);
            }
            return response;
        }
    }
}
