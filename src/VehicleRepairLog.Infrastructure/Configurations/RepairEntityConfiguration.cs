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

            builder.Property(repair => repair.CreatedDate)
                   .IsRequired();

            builder.Property(repair => repair.Description)
                   .HasMaxLength(1000);

            builder.Property(repair => repair.CarWorkshopName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(repair => repair.Name)
                   .HasMaxLength(50);
        }
    }
}
