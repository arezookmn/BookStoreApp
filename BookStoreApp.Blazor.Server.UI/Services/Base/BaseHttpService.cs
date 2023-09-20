using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace BookStoreApp.Blazor.Server.UI.Services.Base
{
    public class BaseHttpService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IClient _client;

        public BaseHttpService(IClient client, ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _client = client;
        }

        protected Response<Guid> ConvertApiException<Guid>(ApiException exception)
        {
            if (exception.StatusCode == 400)
            {
                return new Response<Guid>()
                {
                    Message = "Validation error have occured.", 
                    Success = false,
                    ValidationErrors = exception.Response
                };
            }
            if (exception.StatusCode == 404)
            {
                return new Response<Guid>()
                {
                    Message = "Requested item could not be found.",
                    Success = false
                };
            }

            if (exception.StatusCode >= 200 && exception.StatusCode <= 299)
            {
                return new Response<Guid>()
                {
                    Message = "Operation reported success.",
                    Success = true
                };
            }

            return new Response<Guid>()
            {
                Message = "Something went wrong , try again.",
                Success = false 
            };
        }

        protected async Task GetBearerToken()
        {
            var token = await _localStorage.GetItemAsync<string>("accessToken");
            if (token != null)
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
        }
    }
}
