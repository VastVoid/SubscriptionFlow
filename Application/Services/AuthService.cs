using Application.Models.Auth;
using Application.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services;

public sealed class AuthService(PostgreDbContext dbContext, ISubscriptionsService subscriptionsService) : IAuthService
{
    private ISubscriptionsService _subscriptionsService = subscriptionsService;
    public async Task<AuthResult> LogIn(string login)
    {
        var existsSubscription = await _subscriptionsService.GetAsync(login);
        if (existsSubscription != null)
        {
            return new(true, existsSubscription.Id, existsSubscription.Login);
        }

        var subscriptionId = await _subscriptionsService.AddAsync(new Domain.Entities.Subscription(0, login, 0, null, false, 
            null, DateTimeOffset.Now, DateTimeOffset.Now, null));

        return new(true, subscriptionId, login);
    }
}
