using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Queries.Vehicles
{
    public class GetVehicleByIdQuery : QueryBase<Vehicle>
    {
        public int Id { get; set; }

        public override async Task<Vehicle> Execute(VehicleProfileStorageContext context)
        {
            var vehicle = await context.Vehicles.FirstOrDefaultAsync(x => x.Id == this.Id);
            await context.SaveChangesAsync();
            return vehicle;
        }
    }
}
