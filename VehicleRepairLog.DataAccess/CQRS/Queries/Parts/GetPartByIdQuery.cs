using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Queries.Parts
{
    public class GetPartByIdQuery : QueryBase<Part>
    {
        public int Id { get; set; }

        public async override Task<Part> Execute(VehicleProfileStorageContext context)
        {
            var book = await context.Parts.FirstOrDefaultAsync(x => x.Id == this.Id);
            return book;
        }
    }
}
