using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using VehicleRepairLogUI.Models;

namespace VehicleRepairLogUI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(HttpClient httpClient,
                                     IConfiguration configuration,
                                     ILocalStorageService localStorage,
                                     AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
            httpClient.BaseAddress = new Uri(configuration["ApiUri"]);
        }

        public async Task<RegisterResult> RegisterAsync(RegisterRequest registerRequest)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/Users/register", registerRequest);

            if (response.IsSuccessStatusCode is false)
            {
                return null;
            }

            var registerResult = await response.Content.ReadFromJsonAsync<RegisterResult>();

            return registerResult;
        }

        public async Task<LoginResult> LoginAsync(LoginRequest loginRequest)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/Users/authenticate", loginRequest);
            
            if (response.IsSuccessStatusCode is false)
            {
                return null;
            }

            var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();

            // Adding authentication token to local storage.
            await _localStorage.SetItemAsync("authToken", loginResult.Token);

            // Notifying AuthenticationStateProvider that user state changed and is now authenticated.
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).NotifyUserIsAuthenticated(loginResult.Token);

            // Adding authentication token to HTTP Authorization header as Bearer.
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);

            return loginResult;
        }

        public async Task LogoutAsync()
        {
            // Removes authentication token from local storage.
            await _localStorage.RemoveItemAsync("authToken");

            // Notifies AuthenticationStateProvider that user state changed and is no longer authenticated.
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).NotifyUserLogout();

            // Changes HTTP authorization header to null.
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<LoginResult> ValidateJwt(string token)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/Users/validateJwt", token);

            if (response.IsSuccessStatusCode is false)
            {
                return null;
            }

            var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();
            return loginResult;
        }
    }
}
