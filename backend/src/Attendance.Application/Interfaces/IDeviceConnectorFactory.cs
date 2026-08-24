namespace Attendance.Application.Interfaces;

public interface IDeviceConnectorFactory
{
    IDeviceConnector GetConnector(string vendorCode);
}