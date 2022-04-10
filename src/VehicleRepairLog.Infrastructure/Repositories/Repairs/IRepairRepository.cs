using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Infrastructure.Repositories
{
    public interface IRepairRepository
    {
        Task<Repair> AddAsync(Repair repair, List<string> partNames);
        Task<Repair> GetByIdAsync(int id);
        Task<List<Repair>> GetAllAsync();
        Task UpdateAsync(Repair part);
        Task RemoveAsync(Repair part);
    }
}
