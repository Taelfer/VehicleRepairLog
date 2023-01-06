using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using VehicleRepairLog.Shared.DtoModels;

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

        public async Task<RepairDto> AddRepairAsync(RepairDto repair)
        {
            // Gets authentication Token from local storage.
            string token = await _localStorage.GetItemAsync<string>("authToken");

            // Adds authentication Token to the authentication header.
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/Repairs", repair);

            // HANDLE THIS BETTER!!!
            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            repair = await response.Content.ReadFromJsonAsync<RepairDto>();

            return repair;
        }
    }
}
