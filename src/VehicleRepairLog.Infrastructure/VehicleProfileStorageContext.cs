using Microsoft.EntityFrameworkCore;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Infrastructure
{
    /// <summary>
    /// Determines database context from given properties of type <see cref="DbSet{TEntity}"/>.
    /// </summary>
    public class VehicleProfileStorageContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public VehicleProfileStorageContext(DbContextOptions<VehicleProfileStorageContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<Part> Parts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.Role)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
