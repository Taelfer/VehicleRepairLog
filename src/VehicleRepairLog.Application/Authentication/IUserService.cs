namespace VehicleRepairLog.Application.Authentication
{
    public interface IUserService
    {
        int? GetCurrentUserId();
        string GetCurrentUserRole();
    }
}
