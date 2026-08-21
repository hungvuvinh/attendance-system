using Attendance.Domain.Enums;

namespace Attendance.Domain.Entities;

public sealed class RawAttendanceLog : BaseEntity
{
    public RawAttendanceLog(
        Guid deviceId,
        string employeeDeviceId,
        DateTime timestamp,
        AttendanceDirection direction,
        Guid importBatchId)
    {
        if (deviceId == Guid.Empty)
        {
            throw new ArgumentException("Device id is required.", nameof(deviceId));
        }

        if (string.IsNullOrWhiteSpace(employeeDeviceId))
        {
            throw new ArgumentException("Employee device id is required.", nameof(employeeDeviceId));
        }

        if (importBatchId == Guid.Empty)
        {
            throw new ArgumentException("Import batch id is required.", nameof(importBatchId));
        }

        DeviceId = deviceId;
        EmployeeDeviceId = employeeDeviceId.Trim();
        Timestamp = timestamp;
        Direction = direction;
        ImportBatchId = importBatchId;
    }

    public Guid DeviceId { get; }

    public string EmployeeDeviceId { get; }

    public DateTime Timestamp { get; }

    public AttendanceDirection Direction { get; }

    public Guid ImportBatchId { get; }
}
