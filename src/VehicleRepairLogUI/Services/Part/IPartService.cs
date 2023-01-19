using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLogUI.Services.Part
{
    public interface IPartService
    {
        Task AddPartAsync(PartDto part);
        Task<PartDto> GetPartByIdAsync(int partId);
        Task<PartDto> UpdatePartAsync(PartDto part);
        Task DeletePartAsync(int partId);
    }
}
