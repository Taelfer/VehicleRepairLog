using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using VehicleRepairLogUI.Models;

namespace VehicleRepairLogUI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(HttpClient httpClient,
                                     IConfiguration configuration,
                                     ILocalStorageService localStorage,
                                     AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
            httpClient.BaseAddress = new Uri(configuration["ApiUri"]);
        }

        public async Task<RegisterResult> RegisterAsync(RegisterRequest registerRequest)
        {
            var request = await this.httpClient.PostAsJsonAsync("/api/Users/register", registerRequest);
            var registerResult = await request.Content.ReadFromJsonAsync<RegisterResult>();

            return registerResult;
        }

        public async Task<LoginResult> LoginAsync(LoginRequest loginRequest)
        {
            var request = await this.httpClient.PostAsJsonAsync("/api/Users/authenticate", loginRequest);
            
            if (request.IsSuccessStatusCode is false)
            {
                return null;
            }

            var loginResult = await request.Content.ReadFromJsonAsync<LoginResult>();

            await this.localStorage.SetItemAsync("authToken", loginResult.Token);
            ((CustomAuthenticationStateProvider)this.authenticationStateProvider).NotifyUserIsAuthenticated(loginResult.Token);
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);

            return loginResult;
        }

        public async Task LogoutAsync()
        {
            await this.localStorage.RemoveItemAsync("authToken");
            ((CustomAuthenticationStateProvider)this.authenticationStateProvider).NotifyUserLogout();
            this.httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
