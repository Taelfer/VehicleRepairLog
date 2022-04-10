using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Infrastructure.Repositories
{
    public interface IPartRepository
    {
        Task AddAsync(Part part);
        Task<Part> GetByIdAsync(int id);
        Task<List<Part>> GetAllAsync();
        Task UpdateAsync(Part part);
        Task RemoveAsync(Part part);
    }
}
