using Attendance.Domain.Enums;

namespace Attendance.Application.DTOs;

public sealed record DeviceRawLogDto(
    Guid DeviceId,
    string EmployeeDeviceId,
    DateTime Timestamp,
    AttendanceDirection Direction);