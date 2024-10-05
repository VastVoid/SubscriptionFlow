using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index(nameof(Login), IsUnique = true)]
public sealed class Subscription : EntityBase
{
    public Subscription(long id, string login, short subscriptionStateId, short? planId, bool isActive,
        DateTimeOffset? expiredAt, DateTimeOffset createdAt, DateTimeOffset updatedAt, DateTimeOffset? deletedAt = null)
        : base(id, createdAt, updatedAt, deletedAt)
    {
        Login = login;
        SubscriptionStateId = subscriptionStateId;
        PlanId = planId;
        IsActive = isActive;
        ExpiredAt = expiredAt;
    }

    public string Login { get; set; }
    public short SubscriptionStateId { get; set; }
    public short? PlanId { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset? ExpiredAt { get; set; }
}
