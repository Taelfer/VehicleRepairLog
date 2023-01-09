using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLogUI.Services.User
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> UpdateUserDetailsAsync(UserDto user);
        Task ChangePasswordAsync(PasswordChangeRequestDto passwordChangeRequest, int userId);
    }
}