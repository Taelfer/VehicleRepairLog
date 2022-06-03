using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;
using VehicleRepairLogUI.Models;

namespace VehicleRepairLogUI
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;
        private readonly HttpClient httpClient;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient, IConfiguration configuration)
        {
            this.localStorageService = localStorageService;
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri(configuration["ApiUri"]);
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            User currentUser = await GetUserByJwtAsync();

            if (currentUser is not null && currentUser.EmailAddress is not null)
            {
                //create claims
                var claimEmailAddress = new Claim(ClaimTypes.Name, currentUser.EmailAddress);
                var claimNameIdentifier= new Claim(ClaimTypes.NameIdentifier, Convert.ToString(currentUser.Id));

                //create claimsIdentity
                var claimsIdentity = new ClaimsIdentity(new[] { claimEmailAddress, claimNameIdentifier }, "serverAuth");

                //Create ClaimsPrincipal
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                return new AuthenticationState(claimsPrincipal);
            }
            else
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public async Task<User> GetUserByJwtAsync()
        {
            //getting JWT from localStorage
            var jwtToken = await this.localStorageService.GetItemAsStringAsync("jwtToken");

            if (jwtToken is null)
            {
                return null;
            }

            //HTTP Request to API Endpoint
            var request = await this.httpClient.PostAsJsonAsync("/api/Users/getUserByJwt", jwtToken);

            request.EnsureSuccessStatusCode();

            //HTTP response from API
            var response = await request.Content.ReadFromJsonAsync<User>();

            if (response is not null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }
    }
}
