using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Commands.Repairs
{
    public class AddRepairCommand : CommandBase<Repair, Repair>
    {
        public override async Task<Repair> Execute(VehicleProfileStorageContext context)
        {
            context.Repairs.Add(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
