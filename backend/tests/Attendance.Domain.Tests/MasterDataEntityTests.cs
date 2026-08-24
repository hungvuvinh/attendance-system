using Attendance.Domain.Entities;
using Attendance.Domain.Enums;

namespace Attendance.Domain.Tests;

public class MasterDataEntityTests
{
    [Fact]
    public void Department_constructor_sets_values()
    {
        var department = new Department("HR", "Human Resources", "People operations");

        Assert.Equal("HR", department.Code);
        Assert.Equal("Human Resources", department.Name);
        Assert.Equal("People operations", department.Description);
    }

    [Fact]
    public void Employee_constructor_sets_values()
    {
        var departmentId = Guid.NewGuid();
        var hireDate = new DateOnly(2026, 8, 24);

        var employee = new Employee(
            "NV001",
            "Nguyen Van A",
            departmentId,
            "Engineer",
            hireDate,
            "M001");

        Assert.Equal("NV001", employee.EmployeeCode);
        Assert.Equal("Nguyen Van A", employee.FullName);
        Assert.Equal(departmentId, employee.DepartmentId);
        Assert.Equal("Engineer", employee.Position);
        Assert.Equal(hireDate, employee.HireDate);
        Assert.Null(employee.ResignDate);
        Assert.Equal("M001", employee.AttendanceCode);
        Assert.True(employee.IsActive);
    }

    [Fact]
    public void Employee_constructor_rejects_resign_date_before_hire_date()
    {
        var departmentId = Guid.NewGuid();
        var hireDate = new DateOnly(2026, 8, 24);
        var resignDate = new DateOnly(2026, 8, 23);

        Assert.Throws<ArgumentException>(() => new Employee(
            "NV001",
            "Nguyen Van A",
            departmentId,
            "Engineer",
            hireDate,
            "M001",
            resignDate));
    }

    [Fact]
    public void Employee_constructor_rejects_invalid_attendance_code_format()
    {
        Assert.Throws<ArgumentException>(() => new Employee(
            "NV001",
            "Nguyen Van A",
            Guid.NewGuid(),
            "Engineer",
            new DateOnly(2026, 8, 24),
            "M 001"));
    }

    [Fact]
    public void Employee_unique_invariant_comparison_is_case_insensitive()
    {
        var employee = new Employee(
            "NV001",
            "Nguyen Van A",
            Guid.NewGuid(),
            "Engineer",
            new DateOnly(2026, 8, 24),
            "M001");

        Assert.True(employee.HasSameEmployeeCode(" nv001 "));
    }

    [Fact]
    public void Device_vendor_constructor_sets_values()
    {
        var vendor = new DeviceVendor("MOCK", "Mock Vendor", "Testing vendor");

        Assert.Equal("MOCK", vendor.Code);
        Assert.Equal("Mock Vendor", vendor.Name);
        Assert.Equal("Testing vendor", vendor.Description);
        Assert.True(vendor.IsActive);
    }

    [Fact]
    public void Device_vendor_unique_invariant_comparison_is_case_insensitive()
    {
        var vendor = new DeviceVendor("MOCK", "Mock Vendor", "Testing vendor");

        Assert.True(vendor.HasSameCode(" mock "));
    }

    [Fact]
    public void Department_unique_invariant_comparison_is_case_insensitive()
    {
        var department = new Department("HR", "Human Resources", "People operations");

        Assert.True(department.HasSameCode(" hr "));
    }

    [Fact]
    public void Attendance_device_constructor_sets_values_and_default_connection_status()
    {
        var vendorId = Guid.NewGuid();

        var device = new AttendanceDevice(
            "Main Gate",
            vendorId,
            "K40",
            "SN001",
            "192.168.1.10",
            "Head Office");

        Assert.Equal("Main Gate", device.DeviceName);
        Assert.Equal(vendorId, device.VendorId);
        Assert.Equal("K40", device.Model);
        Assert.Equal("SN001", device.SerialNumber);
        Assert.Equal("192.168.1.10", device.IpAddress);
        Assert.Equal(4370, device.Port);
        Assert.Equal("Head Office", device.Location);
        Assert.True(device.IsActive);
        Assert.Equal(DeviceConnectionStatus.Unknown, device.ConnectionStatus);
        Assert.Null(device.LastSyncAt);
    }

    [Fact]
    public void Attendance_device_unique_invariant_comparison_is_case_insensitive()
    {
        var device = new AttendanceDevice(
            "Main Gate",
            Guid.NewGuid(),
            "K40",
            "SN001",
            "192.168.1.10",
            location: "Head Office");

        Assert.True(device.HasSameSerialNumber(" sn001 "));
    }

    [Fact]
    public void Attendance_device_constructor_rejects_invalid_port()
    {
        var vendorId = Guid.NewGuid();

        Assert.Throws<ArgumentOutOfRangeException>(() => new AttendanceDevice(
            "Main Gate",
            vendorId,
            "K40",
            "SN001",
            "192.168.1.10",
            port: 0,
            location: "Head Office"));
    }
}
