using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Commands.Vehicles
{
    public class UpdateVehicleCommand : CommandBase<Vehicle, Vehicle>
    {
        public override async Task<Vehicle> Execute(VehicleProfileStorageContext context)
        {
            context.Vehicles.Update(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
