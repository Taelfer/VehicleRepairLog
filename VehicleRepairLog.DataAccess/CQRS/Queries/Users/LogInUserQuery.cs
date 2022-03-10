using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Queries.Users
{
    public class LogInUserQuery : QueryBase<User>
    {
        public string Username { get; set; }

        public override Task<User> Execute(VehicleProfileStorageContext context)
        {
            return context.Users.FirstOrDefaultAsync(x => x.Username == this.Username);
        }
    }
}
