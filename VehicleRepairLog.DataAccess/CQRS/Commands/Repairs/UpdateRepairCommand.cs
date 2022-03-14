using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Commands.Repairs
{
    public class UpdateRepairCommand : CommandBase<Repair, Repair>
    {
        public override async Task<Repair> Execute(VehicleProfileStorageContext context)
        {
            context.Repairs.Update(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
