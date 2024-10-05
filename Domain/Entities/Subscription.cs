using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index(nameof(Login), IsUnique = true)]
public sealed class Subscription : EntityBase
{
    public Subscription(long id, string login, short subscriptionStateId, DateTimeOffset createdAt,
        DateTimeOffset updatedAt, DateTimeOffset? deletedAt = null) : base(id, createdAt, updatedAt, deletedAt)
    {
        Login = login;
        SubscriptionStateId = subscriptionStateId;
    }

    public string Login { get; set; }
    public short SubscriptionStateId { get; set; }
}
