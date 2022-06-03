using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using VehicleRepairLogUI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace VehicleRepairLogUI.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly HttpClient httpClient;

        public VehicleService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
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
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Vehicles");
            using var response = await this.httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IEnumerable<Vehicle>>();
        }
    }
}
