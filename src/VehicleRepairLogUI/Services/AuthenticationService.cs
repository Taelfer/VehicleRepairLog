using Blazored.LocalStorage;
using System.Net.Http.Json;
using System.Text.Json;
using VehicleRepairLogUI.Models;

namespace VehicleRepairLogUI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;

        public AuthenticationService(HttpClient httpClient, IConfiguration configuration, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            httpClient.BaseAddress = new Uri(configuration["ApiUri"]);
        }

        public async Task<AuthenticationResponse> AuthenticateJWT(AuthenticationRequest requestModel)//string username, string password)
        {
            var authenticationRequest = new AuthenticationRequest()
            {
                Username = requestModel.Username,
                Password = requestModel.Password
            };

            var request = await this.httpClient.PostAsJsonAsync("/api/Users/authenticate", authenticationRequest);

            if (request.IsSuccessStatusCode is false)
            {
                return null;
            }

            var response = await request.Content.ReadFromJsonAsync<AuthenticationResponse>();

            await localStorage.SetItemAsync("JwtToken", response.Token);

            return response;
        }

        //public Task Login(AuthenticationRequest authenticationRequest)
        //{
        //    this.httpClient.PostAsJsonAsync<AuthenticationRequest>("/");
        //}
    }
}
