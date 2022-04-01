using VehicleRepairLog.Application.Models;

namespace VehicleRepairLog.Application
{
    public interface IUserService
    {
        UserDto GetCurrentUser();
    }
}
