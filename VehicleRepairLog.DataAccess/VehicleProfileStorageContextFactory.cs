using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entites;

namespace VehicleRepairLog.DataAccess
{
    internal class VehicleProfileStorageContextFactory : IDesignTimeDbContextFactory<VehicleProfileStorageContext>
    {
        public VehicleProfileStorageContext CreateDbContext(string[] args)
        {
            var contextOptions = new DbContextOptionsBuilder<VehicleProfileStorageContext>()
                .UseSqlServer("Data Source=DESKTOP-OIBP325;Initial Catalog=VehicleProfileStorage;Integrated Security=True");

            return new VehicleProfileStorageContext(contextOptions.Options);
        }
    }
}
