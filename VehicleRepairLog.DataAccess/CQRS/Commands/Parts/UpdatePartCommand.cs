using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Commands.Parts
{
    public class UpdatePartCommand : CommandBase<Part, Part>
    {
        public override async Task<Part> Execute(VehicleProfileStorageContext context)
        {
            context.Parts.Update(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
