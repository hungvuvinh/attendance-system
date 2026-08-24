using Attendance.Application.DTOs;
using Attendance.Domain.Entities;

namespace Attendance.Application.Interfaces;

public interface IDeviceConnector
{
    string VendorCode { get; }

    Task<DeviceConnectionResultDto> CheckConnectionAsync(
        AttendanceDevice device,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DeviceEmployeeInfoDto>> ReadEmployeesAsync(
        AttendanceDevice device,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DeviceRawLogDto>> ReadAttendanceLogsAsync(
        AttendanceDevice device,
        DateTime? since = null,
        CancellationToken cancellationToken = default);
}