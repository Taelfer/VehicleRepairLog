using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.DataAccess
{
    /// <summary>
    /// Creates database context with given <see cref="DbSet{TEntity}" /> by inheriting <see cref="DbContext" /> class.
    /// </summary>
    public class VehicleProfileStorageContext : DbContext
    {
        public VehicleProfileStorageContext(DbContextOptions<VehicleProfileStorageContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<Part> Parts { get; set; }
    }
}
