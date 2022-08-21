using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using VehicleRepairLogUI.Models;

namespace VehicleRepairLogUI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public UserService(HttpClient httpClient,
                           IConfiguration configuration,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            httpClient.BaseAddress = new Uri(configuration["ApiUri"]);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            string token = await _localStorage.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Users/{id}");

            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            var user = await response.Content.ReadFromJsonAsync<User>();
            return user;
        }
    }
}
