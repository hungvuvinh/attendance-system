using Attendance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attendance.Infrastructure.Persistence.Configurations;

public sealed class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");
        builder.HasKey(department => department.Id);

        builder.Property(department => department.Code)
            .HasColumnName("code")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(department => department.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(department => department.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

        builder.Property(department => department.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(department => department.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(department => department.Code)
            .HasDatabaseName("ix_departments_code")
            .IsUnique();
    }
}