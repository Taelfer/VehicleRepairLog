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
        public string Name { get; set; }

        public override Task<List<Part>> Execute(VehicleProfileStorageContext context)
        {
            if (this.Name != null)
            {
                return context.Parts.Where(x => x.Name == this.Name).ToListAsync();
            }

            return context.Parts.ToListAsync();
        }
    }
}
