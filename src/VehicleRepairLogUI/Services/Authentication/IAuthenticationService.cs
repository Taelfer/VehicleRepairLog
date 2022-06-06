using VehicleRepairLogUI.Models;

namespace VehicleRepairLogUI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<RegisterResult> RegisterAsync(RegisterRequest registerRequest);
        Task<LoginResult> LoginAsync(LoginRequest loginRequest);
        Task LogoutAsync();
        Task<LoginResult> ValidateJwt(string token);
    }
}
