using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Commands.Repairs
{
    public class AddRepairCommand : CommandBase<Repair, Repair>
    {
        public List<string> PartNames { get; set; }
        public override async Task<Repair> Execute(VehicleProfileStorageContext context)
        {
            var parts = await context.Parts.Where(x => PartNames.Contains(x.Name)).ToListAsync();
            this.Parameter.Parts = parts;

            context.Repairs.Add(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
