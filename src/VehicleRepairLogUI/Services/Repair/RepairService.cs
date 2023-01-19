using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using VehicleRepairLog.Shared.DtoModels;
using static System.Net.Mime.MediaTypeNames;

namespace VehicleRepairLogUI.Services.Repair
{
    public class RepairService : IRepairService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public RepairService(HttpClient httpClient,
                              IConfiguration configuration,
                              ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            httpClient.BaseAddress = new Uri(configuration["ApiUri"]);
        }

        public async Task AddRepairAsync(RepairDto repair)
        {
            // Gets authentication Token from local storage.
            string token = await _localStorage.GetItemAsync<string>("authToken");

            // Adds authentication Token to the authentication header.
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/Repairs", repair);

            // HANDLE THIS BETTER!!!
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception();
            }
        }

        public async Task<RepairDto> GetRepairByIdAsync(int repairId)
        {
            string token = await _localStorage.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Repairs/{repairId}");

            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            var repair = await response.Content.ReadFromJsonAsync<RepairDto>();
            return repair;
        }

        public async Task<RepairDto> UpdateRepairAsync(RepairDto repair)
        {
            string token = await _localStorage.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"/api/Repairs/{repair.Id}", repair);

            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            repair = await response.Content.ReadFromJsonAsync<RepairDto>();
            return repair;
        }

        public async Task DeleteRepairAsync(int repairId)
        {
            string token = await _localStorage.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            await _httpClient.DeleteAsync($"/api/Repairs/{repairId}");
        }
    }
}
