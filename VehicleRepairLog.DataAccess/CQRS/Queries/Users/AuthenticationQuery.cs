using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Queries.Users
{
    public class AuthenticationQuery : QueryBase<User>
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public override Task<User> Execute(VehicleProfileStorageContext context)
        {
            if (this.Username is not null)
            {
                return context.Users.FirstOrDefaultAsync(x => x.Username == this.Username);
            }
            else
            {
                return context.Users.FirstOrDefaultAsync(x => x.Email == this.Email);
            }
        }
    }
}
