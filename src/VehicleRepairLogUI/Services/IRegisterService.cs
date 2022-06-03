using VehicleRepairLogUI.Models;

namespace VehicleRepairLogUI.Services
{
    public interface IRegisterService
    {
        Task RegisterUserAsync(User user);
    }
}
