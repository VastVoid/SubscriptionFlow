using Domain.Entities;

namespace Application.Services.Abstractions;

public interface ISubscriptionsService
{
    Task<Subscription?> Get(long id);
    Task<Subscription?> Get(string login);
}
