using Attendance.Domain.Enums;

namespace Attendance.Domain.Entities;

public sealed class DailyAttendance : BaseEntity
{
    public DailyAttendance(Guid employeeId, DateOnly date, AttendanceStatus status)
    {
        if (employeeId == Guid.Empty)
        {
            throw new ArgumentException("Employee id is required.", nameof(employeeId));
        }

        EmployeeId = employeeId;
        Date = date;
        Status = status;
    }

    public Guid EmployeeId { get; }

    public DateOnly Date { get; }

    public AttendanceStatus Status { get; private set; }
}
