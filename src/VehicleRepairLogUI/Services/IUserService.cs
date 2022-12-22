using VehicleRepairLogUI.Models;

namespace VehicleRepairLogUI.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> UpdateUserAsync(User user);
    }
}