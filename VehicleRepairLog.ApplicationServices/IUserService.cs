using VehicleRepairLog.ApplicationServices.API.Domain.Models;

namespace VehicleRepairLog.ApplicationServices
{
    public interface IUserService
    {
        UserDto GetCurrentUser();
    }
}
