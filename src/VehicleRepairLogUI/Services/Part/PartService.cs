using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLogUI.Services.Part
{
    public class PartService : IPartService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public PartService(HttpClient httpClient,
                              IConfiguration configuration,
                              ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            httpClient.BaseAddress = new Uri(configuration["ApiUri"]);
        }

        public async Task AddPartAsync(PartDto part)
        {
            // Gets authentication Token from local storage.
            string token = await _localStorage.GetItemAsync<string>("authToken");

            // Adds authentication Token to the authentication header.
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/Parts", part);

            // HANDLE THIS BETTER!!!
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception();
            }
        }

        public async Task<PartDto> GetPartByIdAsync(int partId)
        {
            string token = await _localStorage.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Parts/{partId}");

            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            var part = await response.Content.ReadFromJsonAsync<PartDto>();
            return part;
        }

        public async Task<PartDto> UpdatePartAsync(PartDto part)
        {
            string token = await _localStorage.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"/api/Parts/{part.Id}", part);

            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            part = await response.Content.ReadFromJsonAsync<PartDto>();
            return part;
        }

        public async Task DeletePartAsync(int partId)
        {
            string token = await _localStorage.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            await _httpClient.DeleteAsync($"/api/Parts/{partId}");
        }
    }
}
