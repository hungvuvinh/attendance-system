using Attendance.Domain.Enums;

namespace Attendance.Domain.Entities;

public sealed class AttendanceDevice : BaseEntity
{
    public AttendanceDevice(
        string deviceName,
        Guid vendorId,
        string model,
        string serialNumber,
        string ipAddress,
        string location,
        int port = 4370,
        DeviceConnectionStatus connectionStatus = DeviceConnectionStatus.Unknown,
        DateTime? lastSyncAt = null)
    {
        if (string.IsNullOrWhiteSpace(deviceName))
        {
            throw new ArgumentException("Device name is required.", nameof(deviceName));
        }

        if (vendorId == Guid.Empty)
        {
            throw new ArgumentException("Vendor id is required.", nameof(vendorId));
        }

        if (string.IsNullOrWhiteSpace(model))
        {
            throw new ArgumentException("Model is required.", nameof(model));
        }

        if (string.IsNullOrWhiteSpace(serialNumber))
        {
            throw new ArgumentException("Serial number is required.", nameof(serialNumber));
        }

        if (string.IsNullOrWhiteSpace(ipAddress))
        {
            throw new ArgumentException("IP address is required.", nameof(ipAddress));
        }

        if (!System.Net.IPAddress.TryParse(ipAddress.Trim(), out _))
        {
            throw new ArgumentException("IP address format is invalid.", nameof(ipAddress));
        }

        if (port is <= 0 or > 65535)
        {
            throw new ArgumentOutOfRangeException(nameof(port), "Port must be in range 1-65535.");
        }

        DeviceName = deviceName.Trim();
        VendorId = vendorId;
        Model = model.Trim();
        SerialNumber = serialNumber.Trim();
        IpAddress = ipAddress.Trim();
        Port = port;
        Location = location?.Trim() ?? string.Empty;
        IsActive = true;
        ConnectionStatus = connectionStatus;
        LastSyncAt = lastSyncAt;
    }

    public string DeviceName { get; private set; }

    public Guid VendorId { get; private set; }

    public string Model { get; private set; }

    public string SerialNumber { get; private set; }

    public string IpAddress { get; private set; }

    public int Port { get; private set; }

    public string Location { get; private set; }

    public bool IsActive { get; private set; }

    public DeviceConnectionStatus ConnectionStatus { get; private set; }

    public DateTime? LastSyncAt { get; private set; }

    public bool HasSameSerialNumber(string serialNumber)
    {
        if (string.IsNullOrWhiteSpace(serialNumber))
        {
            return false;
        }

        return string.Equals(SerialNumber, serialNumber.Trim(), StringComparison.OrdinalIgnoreCase);
    }
}
