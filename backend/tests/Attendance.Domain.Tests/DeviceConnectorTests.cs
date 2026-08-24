using Attendance.Application.Interfaces;
using Attendance.Domain.Entities;
using Attendance.Domain.Enums;
using Attendance.Infrastructure.Connectors;

namespace Attendance.Domain.Tests;

public class DeviceConnectorTests
{
    [Fact]
    public void Factory_resolves_mock_connector_by_vendor_code()
    {
        var factory = new DeviceConnectorFactory(new IDeviceConnector[]
        {
            new MockDeviceConnector()
        });

        var connector = factory.GetConnector(" mock ");

        Assert.IsType<MockDeviceConnector>(connector);
        Assert.Equal("MOCK", connector.VendorCode);
    }

    [Fact]
    public async Task Mock_connector_reports_online_device_with_latency()
    {
        var device = CreateDevice("192.168.1.10");
        var connector = new MockDeviceConnector(TimeSpan.FromMilliseconds(1));

        var result = await connector.CheckConnectionAsync(device);

        Assert.True(result.Connected);
        Assert.Equal(DeviceConnectionStatus.Online, result.ConnectionStatus);
        Assert.True(result.ResponseTimeMs >= 1);
    }

    [Fact]
    public async Task Mock_connector_generates_employees_and_logs()
    {
        var device = CreateDevice("192.168.1.10");
        var connector = new MockDeviceConnector(TimeSpan.Zero);

        var employees = await connector.ReadEmployeesAsync(device);
        var logs = await connector.ReadAttendanceLogsAsync(device, DateTime.UtcNow.AddHours(-1));

        Assert.NotEmpty(employees);
        Assert.All(employees, employee => Assert.False(string.IsNullOrWhiteSpace(employee.DeviceEmployeeId)));
        Assert.NotEmpty(logs);
        Assert.All(logs, log => Assert.Equal(device.Id, log.DeviceId));
    }

    [Fact]
    public void Factory_rejects_unknown_vendor_code()
    {
        var factory = new DeviceConnectorFactory(new IDeviceConnector[]
        {
            new MockDeviceConnector()
        });

        Assert.Throws<NotSupportedException>(() => factory.GetConnector("ZKTECO"));
    }

    private static AttendanceDevice CreateDevice(string ipAddress)
    {
        return new AttendanceDevice(
            "Mock device",
            Guid.NewGuid(),
            "Mock-1",
            "MOCK-001",
            ipAddress,
            "Test office");
    }
}