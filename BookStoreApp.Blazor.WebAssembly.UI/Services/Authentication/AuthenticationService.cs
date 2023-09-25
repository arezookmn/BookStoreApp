using Blazored.LocalStorage;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;
using BookStoreApp.Blazor.WebAssembly.UI.Provider;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Authentication;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly ApiAuthenticationStateProvider _authenticationStateProvider;
        public AuthenticationService(IClient client, ILocalStorageService localStorage, ApiAuthenticationStateProvider authenticationStateProvider)
        {
            _client = client;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(LoginUserDTO loginUserDto)
        {
           var loginResponse = await _client.LoginAsync(loginUserDto);
           if (loginResponse == null) return false;

           //store token
           await _localStorage.SetItemAsync("accessToken", loginResponse.Token);

           //change auth state of app
           await _authenticationStateProvider.LoggedIn();
           return true;
        }

        public async Task Logout()
        { 
            await _authenticationStateProvider.Logout();

        }
    }
}
