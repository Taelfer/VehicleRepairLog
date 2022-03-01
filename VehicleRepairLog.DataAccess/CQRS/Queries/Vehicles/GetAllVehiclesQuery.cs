using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Queries.Vehicles
{
    public class GetAllVehiclesQuery : QueryBase<List<Vehicle>>
    {
        public override Task<List<Vehicle>> Execute(VehicleProfileStorageContext context)
        {
            return context.Vehicles.ToListAsync();
        }
    }
}
