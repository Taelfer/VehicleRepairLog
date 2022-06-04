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
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        public VehicleService(HttpClient httpClient, IConfiguration configuration, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
            httpClient.BaseAddress = new Uri(configuration["ApiUri"]);
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Vehicles");
            request.Content = new StringContent(JsonSerializer.Serialize(vehicle), Encoding.UTF8, Application.Json);
            var response = await this.httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            var token = await this.localStorageService.GetItemAsync<string>("authToken");
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var request = await this.httpClient.GetAsync("/api/Vehicles");

            if (request.IsSuccessStatusCode == false)
            {
                return null;
            }

            var response = await request.Content.ReadFromJsonAsync<IEnumerable<Vehicle>>();

            return response;
        }
    }
}
