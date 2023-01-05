namespace VehicleRepairLog.Application.Authentication
{
    public interface IUserAuthService
    {
        int? GetCurrentUserId();
        string GetCurrentUserRole();
    }
}
