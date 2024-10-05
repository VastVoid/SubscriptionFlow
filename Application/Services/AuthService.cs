using Application.Models.Auth;
using Application.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services;

public sealed class AuthService(PostgreDbContext dbContext) : IAuthService
{
    private PostgreDbContext _dbContext = dbContext;
    public async Task<AuthResult> LogIn(string login)
    {
        var existsSubscription = await _dbContext.Subscribers.FirstOrDefaultAsync(item => item.Login == login);
        if (existsSubscription != null)
        {
            return new(true, existsSubscription.Id, existsSubscription.Login);
        }

        await _dbContext.Subscribers.AddAsync(
            new Domain.Entities.Subscription(0, login, 0, DateTimeOffset.Now, DateTimeOffset.Now));
        var subscriptionId = await _dbContext.SaveChangesAsync();

        return new(true, subscriptionId, login);
    }
}
