using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Infrastructure.Repositories
{
    public class RepairRepository : IRepairRepository
    {
        private readonly VehicleRepairLogContext context;

        public RepairRepository(VehicleRepairLogContext context)
        {
            this.context = context;
        }

        public async Task<Repair> AddAsync(Repair repair, List<string> partNames)
        {
            var parts = await this.context.Parts.Where(part => partNames.Contains(part.Name)).ToListAsync();

            repair.Parts = parts;

            this.context.Repairs.Add(repair);
            await this.context.SaveChangesAsync();
            return repair;
        }

        public Task<List<Repair>> GetAllAsync()
        {
            return this.context.Repairs
                .Include(repair => repair.Parts)
                .ToListAsync();
        }

        public async Task<Repair> GetByIdAsync(int id)
        {
            Repair repair = await this.context.Repairs
            .Include(repair => repair.Parts)
            .FirstOrDefaultAsync(repair => repair.Id == id);

            return repair;
        }

        public Task RemoveAsync(Repair repair)
        {
            this.context.Remove(repair);
            return this.context.SaveChangesAsync();
        }

        public Task UpdateAsync(Repair repair)
        {
            this.context.Update(repair);
            return this.context.SaveChangesAsync();
        }
    }
}
