using Application.Services.Abstractions;
using Domain.Entities;

namespace SubscriptionAutomaton.Common.Services
{
    public sealed class CurrentSubscriptionService
    {
        private readonly ISubscriptionsService _subscriptionsService;
        private readonly ClaimUserService _claimUserService;

        public CurrentSubscriptionService(ISubscriptionsService subscriptionsService, ClaimUserService claimUserService)
        {
            _subscriptionsService = subscriptionsService;
            _claimUserService = claimUserService;
        }

        public async Task<Subscription?> GetAsync()
        {
            var login = await _claimUserService.GetUserNameAsync();
            if (string.IsNullOrEmpty(login))
            {
                return default;
            }

            return await _subscriptionsService.GetAsync(login);
        }
    }
}
