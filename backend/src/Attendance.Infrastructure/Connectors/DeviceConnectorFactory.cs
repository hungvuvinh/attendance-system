using Attendance.Application.Interfaces;

namespace Attendance.Infrastructure.Connectors;

public sealed class DeviceConnectorFactory : IDeviceConnectorFactory
{
    private readonly IReadOnlyDictionary<string, IDeviceConnector> connectors;

    public DeviceConnectorFactory(IEnumerable<IDeviceConnector> connectors)
    {
        ArgumentNullException.ThrowIfNull(connectors);

        this.connectors = connectors.ToDictionary(
            connector => connector.VendorCode.Trim().ToUpperInvariant(),
            StringComparer.OrdinalIgnoreCase);
    }

    public IDeviceConnector GetConnector(string vendorCode)
    {
        if (string.IsNullOrWhiteSpace(vendorCode))
        {
            throw new ArgumentException("Vendor code is required.", nameof(vendorCode));
        }

        var normalizedVendorCode = vendorCode.Trim().ToUpperInvariant();

        return connectors.TryGetValue(normalizedVendorCode, out var connector)
            ? connector
            : throw new NotSupportedException($"No device connector is registered for vendor '{normalizedVendorCode}'.");
    }
}