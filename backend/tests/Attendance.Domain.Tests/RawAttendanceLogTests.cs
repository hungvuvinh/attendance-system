using Attendance.Domain.Entities;
using Attendance.Domain.Enums;

namespace Attendance.Domain.Tests;

public class RawAttendanceLogTests
{
    [Fact]
    public void Constructor_sets_required_values()
    {
        var deviceId = Guid.NewGuid();
        var importBatchId = Guid.NewGuid();
        var timestamp = new DateTime(2026, 8, 20, 7, 52, 15, DateTimeKind.Utc);

        var log = new RawAttendanceLog(
            deviceId,
            "NV001",
            timestamp,
            AttendanceDirection.CheckIn,
            importBatchId);

        Assert.Equal(deviceId, log.DeviceId);
        Assert.Equal("NV001", log.EmployeeDeviceId);
        Assert.Equal(timestamp, log.Timestamp);
        Assert.Equal(AttendanceDirection.CheckIn, log.Direction);
        Assert.Equal(importBatchId, log.ImportBatchId);
    }

    [Fact]
    public void Constructor_rejects_empty_device_id()
    {
        Assert.Throws<ArgumentException>(() => new RawAttendanceLog(
            Guid.Empty,
            "NV001",
            DateTime.UtcNow,
            AttendanceDirection.CheckIn,
            Guid.NewGuid()));
    }

    [Fact]
    public void Constructor_rejects_missing_employee_device_id()
    {
        Assert.Throws<ArgumentException>(() => new RawAttendanceLog(
            Guid.NewGuid(),
            "",
            DateTime.UtcNow,
            AttendanceDirection.CheckIn,
            Guid.NewGuid()));
    }
}
