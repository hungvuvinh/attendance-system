namespace Attendance.Domain.Entities;

public sealed class Employee : BaseEntity
{
    public Employee(
        string employeeCode,
        string fullName,
        Guid departmentId,
        string position,
        DateOnly hireDate,
        string attendanceCode,
        DateOnly? resignDate = null)
    {
        if (string.IsNullOrWhiteSpace(employeeCode))
        {
            throw new ArgumentException("Employee code is required.", nameof(employeeCode));
        }

        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("Full name is required.", nameof(fullName));
        }

        if (departmentId == Guid.Empty)
        {
            throw new ArgumentException("Department id is required.", nameof(departmentId));
        }

        if (string.IsNullOrWhiteSpace(position))
        {
            throw new ArgumentException("Position is required.", nameof(position));
        }

        if (string.IsNullOrWhiteSpace(attendanceCode))
        {
            throw new ArgumentException("Attendance code is required.", nameof(attendanceCode));
        }

        var normalizedAttendanceCode = attendanceCode.Trim();

        if (!IsValidAttendanceCode(normalizedAttendanceCode))
        {
            throw new ArgumentException(
                "Attendance code format is invalid. Use only letters, digits, underscore or hyphen.",
                nameof(attendanceCode));
        }

        if (resignDate.HasValue && resignDate.Value < hireDate)
        {
            throw new ArgumentException("Resign date cannot be earlier than hire date.", nameof(resignDate));
        }

        EmployeeCode = employeeCode.Trim();
        FullName = fullName.Trim();
        DepartmentId = departmentId;
        Position = position.Trim();
        HireDate = hireDate;
        ResignDate = resignDate;
        AttendanceCode = normalizedAttendanceCode;
        IsActive = true;
    }

    public string EmployeeCode { get; private set; }

    public string FullName { get; private set; }

    public Guid DepartmentId { get; private set; }

    public string Position { get; private set; }

    public DateOnly HireDate { get; private set; }

    public DateOnly? ResignDate { get; private set; }

    public string AttendanceCode { get; private set; }

    public bool IsActive { get; private set; }

    public bool HasSameEmployeeCode(string employeeCode)
    {
        if (string.IsNullOrWhiteSpace(employeeCode))
        {
            return false;
        }

        return string.Equals(EmployeeCode, employeeCode.Trim(), StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsValidAttendanceCode(string value)
    {
        if (value.Length > 50)
        {
            return false;
        }

        foreach (var ch in value)
        {
            if (!char.IsLetterOrDigit(ch) && ch != '_' && ch != '-')
            {
                return false;
            }
        }

        return true;
    }
}
