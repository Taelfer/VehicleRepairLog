using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Infrastructure.Configurations
{
    public class VehicleEntityConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(vehicle => vehicle.Id);

            builder.Property(vehicle => vehicle.Name)
                   .HasMaxLength(25);

            builder.Property(vehicle => vehicle.BrandName)
                   .HasMaxLength(25);

            builder.Property(vehicle => vehicle.VinNumber)
                   .IsRequired()
                   .HasMaxLength(25);

            builder.Property(vehicle => vehicle.PaintColor)
                   .HasMaxLength(20);

            builder.Property(vehicle => vehicle.FuelType)
                   .HasMaxLength(20);
        }
    }
}
