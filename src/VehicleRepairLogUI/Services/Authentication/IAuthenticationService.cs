using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLogUI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<RegisterResultDto> RegisterAsync(RegisterRequestDto registerRequest);
        Task<LoginResultDto> LoginAsync(LoginRequestDto loginRequest);
        Task LogoutAsync();
    }
}
