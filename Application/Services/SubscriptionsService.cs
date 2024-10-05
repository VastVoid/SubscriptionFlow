using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services;

public sealed class SubscriptionsService(PostgreDbContext dbContext)
{
    private readonly PostgreDbContext _dbContext = dbContext;

    public async Task<Subscription?> Get(long id)
    {
        return await _dbContext.Subscribers.FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<Subscription?> Get(string login)
    {
        return await _dbContext.Subscribers.FirstOrDefaultAsync(item => item.Login == login);
    }
}
