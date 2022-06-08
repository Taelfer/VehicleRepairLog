using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Infrastructure.Configurations
{
    public class RepairEntityConfiguration : IEntityTypeConfiguration<Repair>
    {
        public void Configure(EntityTypeBuilder<Repair> builder)
        {
            builder.HasKey(repair => repair.Id);

            builder.Property(repair => repair.Date)
                   .IsRequired();

            builder.Property(repair => repair.Description)
                   .HasMaxLength(1000);

            builder.Property(repair => repair.CarWorkshopName)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
