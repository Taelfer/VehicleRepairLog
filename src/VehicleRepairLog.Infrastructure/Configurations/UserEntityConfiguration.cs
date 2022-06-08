using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Infrastructure.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.Property(user => user.FirstName)
                   .HasMaxLength(50);

            builder.Property(user => user.LastName)
                   .HasMaxLength(50);

            builder.Property(user => user.Email)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(user => user.Role)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(user => user.Username)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(user => user.Password)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
