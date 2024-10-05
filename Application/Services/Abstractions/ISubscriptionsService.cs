using Domain.Entities;

namespace Application.Services.Abstractions;

public interface ISubscriptionsService
{
    Task<Subscription?> GetAsync(long id);
    Task<Subscription?> GetAsync(string login);
    Task<long> AddAsync(Subscription subscription);
    Task<long> UpdateAsync(Subscription subscription);
}
