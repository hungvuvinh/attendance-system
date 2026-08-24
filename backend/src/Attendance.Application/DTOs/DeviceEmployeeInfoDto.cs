namespace Attendance.Application.DTOs;

public sealed record DeviceEmployeeInfoDto(
    string DeviceEmployeeId,
    string FullName,
    string? AttendanceCode = null);