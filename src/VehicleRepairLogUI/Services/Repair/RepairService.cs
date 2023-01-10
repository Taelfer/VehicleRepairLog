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
    }
}
