using VehicleRepairLogUI.Models;

namespace VehicleRepairLogUI.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
    }
}