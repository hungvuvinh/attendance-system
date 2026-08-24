namespace Attendance.Domain.Entities;

public sealed class DeviceVendor : BaseEntity
{
    public DeviceVendor(string code, string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Vendor code is required.", nameof(code));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Vendor name is required.", nameof(name));
        }

        Code = code.Trim();
        Name = name.Trim();
        Description = description?.Trim() ?? string.Empty;
        IsActive = true;
    }

    public string Code { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public bool IsActive { get; private set; }

    public bool HasSameCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return false;
        }

        return string.Equals(Code, code.Trim(), StringComparison.OrdinalIgnoreCase);
    }
}