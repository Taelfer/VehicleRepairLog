using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using VehicleRepairLogUI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace VehicleRepairLogUI.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly HttpClient httpClient;

        public RegisterService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri(configuration["ApiUri"]);
        }

        public async Task RegisterUserAsync(User user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Users/register");
            request.Content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, Application.Json);
            var response = await this.httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
