using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using VehicleRepairLogUI.Models;
using static System.Net.Mime.MediaTypeNames;
using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace VehicleRepairLogUI.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public VehicleService(HttpClient httpClient, IConfiguration configuration, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            httpClient.BaseAddress = new Uri(configuration["ApiUri"]);
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Vehicles");
            request.Content = new StringContent(JsonSerializer.Serialize(vehicle), Encoding.UTF8, Application.Json);
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            // Getting authentication token from local storage and adding it to HTTP request authorization header as Bearer.
            string token = await _localStorage.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync("/api/Vehicles");

            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            var vehicles = await response.Content.ReadFromJsonAsync<IEnumerable<Vehicle>>();

            return vehicles;
        }
    }
}
