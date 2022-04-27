using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Application.Authentication
{
    public interface IJwtAuth
    {
        string GenerateToken(User user);
    }
}
