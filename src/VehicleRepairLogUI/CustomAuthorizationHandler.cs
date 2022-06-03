using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace VehicleRepairLogUI
{
    public class CustomAuthorizationHandler : DelegatingHandler
    {
        private readonly ILocalStorageService localStorageService;

        public CustomAuthorizationHandler(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwtToken = await this.localStorageService.GetItemAsStringAsync("jwtToken");

            //adding jwt in authorization header
            if (jwtToken is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
