namespace Domain.Entities;

public abstract class EntityBase
{
    protected EntityBase(long id, DateTimeOffset createdAt, DateTimeOffset updatedAt,
        DateTimeOffset? deletedAt)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        DeletedAt = deletedAt;
    }

    public long Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
