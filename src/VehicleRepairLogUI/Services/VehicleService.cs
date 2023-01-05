using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using VehicleRepairLog.Shared.DtoModels;
using static System.Net.Mime.MediaTypeNames;

namespace VehicleRepairLogUI.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public VehicleService(HttpClient httpClient,
                              IConfiguration configuration,
                              ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            httpClient.BaseAddress = new Uri(configuration["ApiUri"]);
        }

        public async Task AddVehicleAsync(VehicleDto vehicle)
        {
            string token = await _localStorage.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Vehicles");
            request.Content = new StringContent(JsonSerializer.Serialize(vehicle), Encoding.UTF8, Application.Json);
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception();
            }
        }

        public async Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync()
        {
            // Getting authentication token from local storage and adding it to HTTP request authorization header as Bearer.
            string token = await _localStorage.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync("/api/Vehicles");

            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            var vehicles = await response.Content.ReadFromJsonAsync<IEnumerable<VehicleDto>>();

            return vehicles;
        }

        public async Task<VehicleDto> GetVehicleByIdAsync(int id)
        {
            string token = await _localStorage.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Vehicles/{id}");

            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            var vehicle = await response.Content.ReadFromJsonAsync<VehicleDto>();
            return vehicle;
        }

        public async Task<VehicleDto> UpdateVehicleAsync(VehicleDto vehicle)
        {
            string token = await _localStorage.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"/api/Vehicles/{vehicle.Id}", vehicle);

            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            vehicle = await response.Content.ReadFromJsonAsync<VehicleDto>();
            return vehicle;
        }

        public async Task DeleteVehicleAsync(int vehicleId)
        {
            string token = await _localStorage.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            await _httpClient.DeleteAsync($"/api/Vehicles/{vehicleId}");
        }
    }
}
