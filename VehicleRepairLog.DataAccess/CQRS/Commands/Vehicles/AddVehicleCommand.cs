using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess.CQRS.Commands.Vehicles
{
    public class AddVehicleCommand : CommandBase<Vehicle, Vehicle>
    {
        public override async Task<Vehicle> Execute(VehicleProfileStorageContext context)
        {
            context.Vehicles.Add(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
