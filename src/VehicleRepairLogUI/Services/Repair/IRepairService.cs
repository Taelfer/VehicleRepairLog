using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLogUI.Services.Repair
{
    public interface IRepairService
    {
        Task<RepairDto> AddRepairAsync(RepairDto repair);
    }
}
