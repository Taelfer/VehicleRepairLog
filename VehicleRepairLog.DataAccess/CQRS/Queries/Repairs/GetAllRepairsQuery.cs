using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Queries.Repairs
{
    public class GetAllRepairsQuery : QueryBase<List<Repair>>
    {
        public override Task<List<Repair>> Execute(VehicleProfileStorageContext context)
        {
            return context.Repairs.ToListAsync();
        }
    }
}
