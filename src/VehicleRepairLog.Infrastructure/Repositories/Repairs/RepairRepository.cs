using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Infrastructure.Repositories
{
    public class RepairRepository : IRepairRepository
    {
        private readonly VehicleProfileStorageContext context;

        public RepairRepository(VehicleProfileStorageContext context)
        {
            this.context = context;
        }

        public async Task<Repair> AddAsync(Repair repair, List<string> partNames)
        {
            var parts = await this.context.Parts.Where(x => partNames.Contains(x.Name)).ToListAsync();

            repair.Parts = parts;

            this.context.Repairs.Add(repair);
            await this.context.SaveChangesAsync();
            return repair;
        }

        public Task<List<Repair>> GetAllAsync()
        {
            return this.context.Repairs
                .Include(x => x.Parts)
                .ToListAsync();
        }

        public async Task<Repair> GetByIdAsync(int id)
        {
            var part = await this.context.Repairs
                .Include(x => x.Parts)
                .FirstOrDefaultAsync(x => x.Id == id);

            return part;
        }

        public Task RemoveAsync(Repair part)
        {
            this.context.Remove(part);
            return this.context.SaveChangesAsync();
        }

        public Task UpdateAsync(Repair part)
        {
            this.context.Update(part);
            return this.context.SaveChangesAsync();
        }
    }
}
