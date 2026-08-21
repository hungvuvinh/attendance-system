namespace Attendance.Domain.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public Guid Id { get; protected init; }

    public DateTime CreatedAt { get; protected init; }

    public DateTime UpdatedAt { get; protected set; }
}
