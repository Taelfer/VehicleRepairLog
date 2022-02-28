using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Commands.Parts
{
    public class DeletePartCommand : CommandBase<Part, Part>
    {
        public override async Task<Part> Execute(VehicleProfileStorageContext context)
        {
            context.Parts.Remove(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
