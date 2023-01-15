using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Infrastructure.Configurations
{
    public class PartEntityConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.HasKey(part => part.Id);

            builder.Property(part => part.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(part => part.BrandName)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
