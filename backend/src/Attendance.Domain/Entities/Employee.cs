namespace Attendance.Domain.Entities;

public sealed class Employee : BaseEntity
{
    public Employee(string employeeCode, string fullName, Guid departmentId)
    {
        if (string.IsNullOrWhiteSpace(employeeCode))
        {
            throw new ArgumentException("Employee code is required.", nameof(employeeCode));
        }

        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("Full name is required.", nameof(fullName));
        }

        EmployeeCode = employeeCode.Trim();
        FullName = fullName.Trim();
        DepartmentId = departmentId;
        IsActive = true;
    }

    public string EmployeeCode { get; private set; }

    public string FullName { get; private set; }

    public Guid DepartmentId { get; private set; }

    public bool IsActive { get; private set; }
}
