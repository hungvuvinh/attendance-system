using Attendance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attendance.Infrastructure.Persistence.Configurations;

public sealed class DeviceVendorConfiguration : IEntityTypeConfiguration<DeviceVendor>
{
    public void Configure(EntityTypeBuilder<DeviceVendor> builder)
    {
        builder.ToTable("device_vendors");
        builder.HasKey(vendor => vendor.Id);

        builder.Property(vendor => vendor.Code)
            .HasColumnName("code")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(vendor => vendor.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(vendor => vendor.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

        builder.Property(vendor => vendor.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(vendor => vendor.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(vendor => vendor.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(vendor => vendor.Code)
            .HasDatabaseName("ix_device_vendors_code")
            .IsUnique();
    }
}