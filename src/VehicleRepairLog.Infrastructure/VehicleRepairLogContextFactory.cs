using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VehicleRepairLog.Infrastructure
{
    internal class VehicleRepairLogContextFactory : IDesignTimeDbContextFactory<VehicleRepairLogContext>
    {
        public VehicleRepairLogContext CreateDbContext(string[] args)
        {
            var contextOptions = new DbContextOptionsBuilder<VehicleRepairLogContext>()
                .UseSqlServer("Data Source=DESKTOP-OIBP325;Initial Catalog=VehicleRepairLog;Integrated Security=True");

            return new VehicleRepairLogContext(contextOptions.Options);
        }
    }
}
