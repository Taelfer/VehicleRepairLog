using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLogUI.Services.Repair
{
    public interface IRepairService
    {
        Task AddRepairAsync(RepairDto repair);
        Task<RepairDto> GetRepairByIdAsync(int repairId);
        Task<RepairDto> UpdateRepairAsync(RepairDto repair);
        Task DeleteRepairAsync(int repairId);
    }
}
