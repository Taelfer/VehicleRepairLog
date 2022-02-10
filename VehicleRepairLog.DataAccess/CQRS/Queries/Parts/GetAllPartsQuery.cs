using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Queries.Parts
{
    public class GetAllPartsQuery : QueryBase<List<Part>>
    {
        public override Task<List<Part>> Execute(VehicleProfileStorageContext context)
        {
            return context.Parts.ToListAsync();
        }
    }
}
