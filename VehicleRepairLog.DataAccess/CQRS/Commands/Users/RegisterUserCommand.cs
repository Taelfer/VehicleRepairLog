using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Commands.Users
{
    public class RegisterUserCommand : CommandBase<User, User>
    {
        public override async Task<User> Execute(VehicleProfileStorageContext context)
        {
            context.Users.Add(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
