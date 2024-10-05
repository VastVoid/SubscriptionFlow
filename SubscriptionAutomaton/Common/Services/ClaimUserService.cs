using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace SubscriptionAutomaton.Common.Services;

public sealed class ClaimUserService
{
    private readonly AuthenticationStateProvider _authStateProvider;

    public ClaimUserService(AuthenticationStateProvider authenticationStateProvider)
    {
        _authStateProvider = authenticationStateProvider;
    }

    public async Task<ClaimsPrincipal> GetUserAsync()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        return authState.User;
    }

    public async Task<string?> GetUserNameAsync()
    {
        var user = await GetUserAsync();
        return user.Identity?.IsAuthenticated == true
            ? user.FindFirst(c => c.Type == ClaimTypes.Name)?.Value ?? default
            : default;
    }

    public async Task<bool> IsUserAuthenticatedAsync()
    {
        var user = await GetUserAsync();
        return user.Identity?.IsAuthenticated == true;
    }
}
