using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Queries.Repairs
{
    public class GetRepairByIdQuery : QueryBase<Repair>
    {
        public int Id { get; set; }

        public async override Task<Repair> Execute(VehicleProfileStorageContext context)
        {
            var repair = await context.Repairs
                .Include(x => x.Parts)
                .FirstOrDefaultAsync(x => x.Id == this.Id);

            return repair;
        }
    }
}
