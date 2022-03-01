using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Commands.Vehicles
{
    public class DeleteVehicleCommand : CommandBase<Vehicle, Vehicle>
    {
        public override async Task<Vehicle> Execute(VehicleProfileStorageContext context)
        {
            context.Vehicles.Remove(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
