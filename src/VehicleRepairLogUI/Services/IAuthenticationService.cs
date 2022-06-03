using VehicleRepairLogUI.Models;

namespace VehicleRepairLogUI.Services
{
    public interface IAuthenticationService
    {
        //Task Login();
        Task<AuthenticationResponse> AuthenticateJWT(AuthenticationRequest requestModel);//string username, string password);
    }
}
