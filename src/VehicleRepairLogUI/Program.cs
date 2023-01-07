using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VehicleRepairLogUI.Services.Authentication;
using VehicleRepairLogUI.Services.Repair;
using VehicleRepairLogUI.Services.User;
using VehicleRepairLogUI.Services.Vehicle;

namespace VehicleRepairLogUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Adding DI configuration for HttpClient implementation.
            builder.Services
                .AddScoped<IVehicleService, VehicleService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAuthenticationService, AuthenticationService>();

            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            builder.Services.AddBlazoredLocalStorage();

            // Creating 'typed' HttpClient instances for Services.
            builder.Services.AddHttpClient<IVehicleService, VehicleService>();
            builder.Services.AddHttpClient<IUserService, UserService>();
            builder.Services.AddHttpClient<IRepairService, RepairService>();
            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>();
            builder.Services.AddHttpClient<CustomAuthenticationStateProvider>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(builder.Configuration["ApiUri"]);
            });

            //// Example of 'named' HttpClient instance.
            //builder.Services.AddHttpClient("MyWebApi", httpClient =>
            //{
            //    httpClient.BaseAddress = new Uri(builder.Configuration["ApiUri"]);
            //});

            await builder.Build().RunAsync();
        }
    }
}