using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLogUI.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync();
        Task<VehicleDto> GetVehicleByIdAsync(int id);
        Task AddVehicleAsync(VehicleDto vehicle);
        Task<VehicleDto> UpdateVehicleAsync(VehicleDto vehicle);
        Task DeleteVehicleAsync(int vehicleId);
    }
}
