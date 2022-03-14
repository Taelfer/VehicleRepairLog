using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Queries.Users
{
    public class GetUserByIdQuery : QueryBase<User>
    {
        public int Id { get; set; }

        public override async Task<User> Execute(VehicleProfileStorageContext context)
        {
            var user = await context.Users
                .Include(x => x.Vehicles)
                .FirstOrDefaultAsync(x => x.Id == this.Id);

            return user;
        }
    }
}
