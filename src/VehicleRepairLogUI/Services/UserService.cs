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

        /// <summary>
        /// Calls API in order to get data for User of given ID
        /// </summary>
        /// <param name="id">Id of the user the data will be returned for</param>
        /// <returns>Object with user data of type <see cref="User"/></returns>
        public async Task<User> GetUserByIdAsync(int id)
        {
            // Gets authentication Token from local storage.
            string token = await _localStorage.GetItemAsync<string>("authToken");

            // Adds authentication Token to the authentication header.
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Calls the API.
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Users/{id}");

            // If status code returned by API false...
            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            // Otherwise map data returned by API into User object.
            var user = await response.Content.ReadFromJsonAsync<User>();

            return user;
        }
    }
}
