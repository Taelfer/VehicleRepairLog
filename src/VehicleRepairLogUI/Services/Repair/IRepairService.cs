using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLogUI.Services.Repair
{
    public interface IRepairService
    {
        Task AddRepairAsync(RepairDto repair);
        Task DeleteRepairAsync(int repairId);
    }
}
