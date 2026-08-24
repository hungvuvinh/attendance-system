using Attendance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attendance.Infrastructure.Persistence.Configurations;

public sealed class AttendanceDeviceConfiguration : IEntityTypeConfiguration<AttendanceDevice>
{
    public void Configure(EntityTypeBuilder<AttendanceDevice> builder)
    {
        builder.ToTable("attendance_devices");
        builder.HasKey(device => device.Id);

        builder.Property(device => device.DeviceName)
            .HasColumnName("device_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(device => device.VendorId)
            .HasColumnName("vendor_id")
            .IsRequired();

        builder.Property(device => device.Model)
            .HasColumnName("model")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(device => device.SerialNumber)
            .HasColumnName("serial_number")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(device => device.IpAddress)
            .HasColumnName("ip_address")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(device => device.Port)
            .HasColumnName("port")
            .HasDefaultValue(4370)
            .IsRequired();

        builder.Property(device => device.Location)
            .HasColumnName("location")
            .HasMaxLength(255);

        builder.Property(device => device.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(device => device.ConnectionStatus)
            .HasColumnName("connection_status")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(device => device.LastSyncAt)
            .HasColumnName("last_sync_at");

        builder.Property(device => device.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(device => device.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(device => device.SerialNumber)
            .HasDatabaseName("ix_attendance_devices_serial_number")
            .IsUnique();

        builder.HasIndex(device => device.VendorId)
            .HasDatabaseName("ix_attendance_devices_vendor_id");

        builder.HasOne<DeviceVendor>()
            .WithMany()
            .HasForeignKey(device => device.VendorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}