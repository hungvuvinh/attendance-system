using Attendance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attendance.Infrastructure.Persistence.Configurations;

public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");
        builder.HasKey(employee => employee.Id);

        builder.Property(employee => employee.EmployeeCode)
            .HasColumnName("employee_code")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(employee => employee.FullName)
            .HasColumnName("full_name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(employee => employee.DepartmentId)
            .HasColumnName("department_id")
            .IsRequired();

        builder.Property(employee => employee.Position)
            .HasColumnName("position")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(employee => employee.HireDate)
            .HasColumnName("hire_date")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(employee => employee.ResignDate)
            .HasColumnName("resign_date")
            .HasColumnType("date");

        builder.Property(employee => employee.AttendanceCode)
            .HasColumnName("attendance_code")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(employee => employee.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(employee => employee.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(employee => employee.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(employee => employee.EmployeeCode)
            .HasDatabaseName("ix_employees_employee_code")
            .IsUnique();

        builder.HasIndex(employee => employee.AttendanceCode)
            .HasDatabaseName("ix_employees_attendance_code");

        builder.HasIndex(employee => employee.DepartmentId)
            .HasDatabaseName("ix_employees_department_id");

        builder.HasOne<Department>()
            .WithMany()
            .HasForeignKey(employee => employee.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}