using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Queries.Users
{
    public class GetAllUsersQuery : QueryBase<List<User>>
    {
        public override Task<List<User>> Execute(VehicleProfileStorageContext context)
        {
            return context.Users.ToListAsync();
        }
    }
}
