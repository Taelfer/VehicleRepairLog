using Microsoft.EntityFrameworkCore;
using VehicleRepairLog.Infrastructure.Configurations;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Infrastructure
{
    /// <summary>
    /// Determines database context from given properties of type <see cref="DbSet{TEntity}"/>.
    /// </summary>
    public class VehicleRepairLogContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public VehicleRepairLogContext(DbContextOptions<VehicleRepairLogContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<Part> Parts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserEntityConfiguration().Configure(modelBuilder.Entity<User>());
            new VehicleEntityConfiguration().Configure(modelBuilder.Entity<Vehicle>());
            new RepairEntityConfiguration().Configure(modelBuilder.Entity<Repair>());
            new PartEntityConfiguration().Configure(modelBuilder.Entity<Part>());
        }
    }
}
