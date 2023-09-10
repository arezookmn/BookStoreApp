using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using BookStoreApp.Blazor.Server.UI.Provider;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
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
