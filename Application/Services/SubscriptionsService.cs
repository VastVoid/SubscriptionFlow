using Application.Services.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services;

public sealed class SubscriptionsService(PostgreDbContext dbContext) : ISubscriptionsService
{
    private readonly PostgreDbContext _dbContext = dbContext;

    public async Task<Subscription?> GetAsync(long id)
    {
        return await _dbContext.Subscribers.FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<Subscription?> GetAsync(string login)
    {
        return await _dbContext.Subscribers.FirstOrDefaultAsync(item => item.Login == login);
    }

    public async Task<long> AddAsync(Subscription subscription)
    {
        if (subscription.Id > 0)
        {
            throw new ArgumentException($"cannot add {nameof(subscription)}, because {nameof(subscription.Id)} must be default value");
        }

        await _dbContext.Subscribers.AddAsync(subscription);
        await _dbContext.SaveChangesAsync();
        return subscription.Id;
    }

    public async Task<long> UpdateAsync(Subscription subscription)
    {
        var exists = subscription.Id > 0 && await _dbContext.Subscribers.AnyAsync(item => item.Id == subscription.Id);
        if (!exists)
        {
            throw new ArgumentException($"{nameof(subscription)} with id = {subscription.Id} is not exists");
        }

        subscription.UpdatedAt = DateTime.Now;
        _dbContext.Subscribers.Update(subscription);

        await _dbContext.SaveChangesAsync();
        return subscription.Id;
    }
}
