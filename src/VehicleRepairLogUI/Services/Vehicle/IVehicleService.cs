using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLogUI.Services.Vehicle
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync();
        Task<VehicleDto> GetVehicleByIdAsync(int vehicleId);
        Task AddVehicleAsync(VehicleDto vehicle);
        Task<VehicleDto> UpdateVehicleAsync(VehicleDto vehicle);
        Task DeleteVehicleAsync(int vehicleId);
    }
}
