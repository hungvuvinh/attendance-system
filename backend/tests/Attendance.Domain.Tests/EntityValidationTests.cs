using Attendance.Domain.Entities;
using Attendance.Domain.Enums;

namespace Attendance.Domain.Tests;

public class EntityValidationTests
{
    [Fact]
    public void Attendance_status_contains_the_documented_values()
    {
        var expected = new[]
        {
            "PRESENT",
            "ABSENT",
            "LATE",
            "EARLY_LEAVE",
            "LATE_AND_EARLY",
            "MISSING_CHECKIN",
            "MISSING_CHECKOUT",
            "NO_SHIFT",
            "LEAVE",
            "HOLIDAY",
            "BUSINESS_TRIP",
            "REMOTE_WORK",
            "OVERTIME"
        };

        Assert.Equal(expected, Enum.GetNames<AttendanceStatus>());
    }

    [Fact]
    public void Base_entity_has_identity_and_audit_timestamps()
    {
        var entity = new Employee(
            "NV001",
            "Nguyen Van A",
            Guid.NewGuid(),
            "Engineer",
            new DateOnly(2026, 8, 20),
            "M001");

        Assert.NotEqual(Guid.Empty, entity.Id);
        Assert.NotEqual(default, entity.CreatedAt);
        Assert.Equal(entity.CreatedAt, entity.UpdatedAt);
    }
}
