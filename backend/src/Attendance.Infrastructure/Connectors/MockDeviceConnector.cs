using System.Diagnostics;
using Attendance.Application.DTOs;
using Attendance.Application.Interfaces;
using Attendance.Domain.Entities;
using Attendance.Domain.Enums;

namespace Attendance.Infrastructure.Connectors;

public sealed class MockDeviceConnector : IDeviceConnector
{
    private static readonly IReadOnlyList<DeviceEmployeeInfoDto> MockEmployees =
    [
        new("MOCK-001", "Nguyen Van A", "M001"),
        new("MOCK-002", "Tran Thi B", "M002")
    ];

    private readonly TimeSpan networkLatency;

    public MockDeviceConnector(TimeSpan? networkLatency = null)
    {
        if (networkLatency.HasValue && networkLatency.Value < TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(networkLatency), "Network latency cannot be negative.");
        }

        this.networkLatency = networkLatency ?? TimeSpan.FromMilliseconds(50);
    }

    public string VendorCode => "MOCK";

    public async Task<DeviceConnectionResultDto> CheckConnectionAsync(
        AttendanceDevice device,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(device);

        var stopwatch = Stopwatch.StartNew();
        await SimulateNetworkLatencyAsync(cancellationToken);

        var connected = device.IpAddress != "0.0.0.0";
        stopwatch.Stop();

        return new DeviceConnectionResultDto(
            connected,
            (int)stopwatch.ElapsedMilliseconds,
            connected ? DeviceConnectionStatus.Online : DeviceConnectionStatus.Offline,
            connected ? null : "Mock device is offline.");
    }

    public async Task<IReadOnlyList<DeviceEmployeeInfoDto>> ReadEmployeesAsync(
        AttendanceDevice device,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(device);
        await SimulateNetworkLatencyAsync(cancellationToken);
        return MockEmployees;
    }

    public async Task<IReadOnlyList<DeviceRawLogDto>> ReadAttendanceLogsAsync(
        AttendanceDevice device,
        DateTime? since = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(device);
        await SimulateNetworkLatencyAsync(cancellationToken);

        var now = DateTime.UtcNow;
        var logs = new List<DeviceRawLogDto>
        {
            new(device.Id, "MOCK-001", now.AddMinutes(-15), AttendanceDirection.CheckIn),
            new(device.Id, "MOCK-002", now.AddMinutes(-5), AttendanceDirection.CheckIn),
            new(device.Id, "MOCK-001", now, AttendanceDirection.CheckOut)
        };

        return since.HasValue
            ? logs.Where(log => log.Timestamp >= since.Value).ToArray()
            : logs;
    }

    private async Task SimulateNetworkLatencyAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(networkLatency, cancellationToken);
    }
}