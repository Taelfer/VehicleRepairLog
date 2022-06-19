using VehicleRepairLogUI.Models;

namespace VehicleRepairLogUI.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
        Task<Vehicle> GetVehicleByIdAsync(int id);
        Task AddVehicleAsync(Vehicle vehicle);
        Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(int vehicleId);
    }
}
