using Attendance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Attendance.Application.Interfaces;

public interface IAppDbContext
{
    DbSet<Department> Departments { get; }

    DbSet<Employee> Employees { get; }

    DbSet<DeviceVendor> DeviceVendors { get; }

    DbSet<AttendanceDevice> AttendanceDevices { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}