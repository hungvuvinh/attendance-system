namespace Attendance.Domain.Entities;

public sealed class AttendanceDevice : BaseEntity
{
    public AttendanceDevice(string deviceName, string serialNumber, string location)
    {
        if (string.IsNullOrWhiteSpace(deviceName))
        {
            throw new ArgumentException("Device name is required.", nameof(deviceName));
        }

        if (string.IsNullOrWhiteSpace(serialNumber))
        {
            throw new ArgumentException("Serial number is required.", nameof(serialNumber));
        }

        DeviceName = deviceName.Trim();
        SerialNumber = serialNumber.Trim();
        Location = location?.Trim() ?? string.Empty;
        IsActive = true;
    }

    public string DeviceName { get; private set; }

    public string SerialNumber { get; private set; }

    public string Location { get; private set; }

    public bool IsActive { get; private set; }
}
