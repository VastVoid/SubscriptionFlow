using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace SubscriptionAutomaton.Extensions.Sessions
{
    public static class SessionExtensions
    {
        private const string LoginKey = "login";
        private const string SubscriptionIdKey = "subscriptionId";
        private const string IsAuthenticatedKey = "isAuthenticated";

        public static async Task LogIn(this ProtectedSessionStorage storage, string login,
            long subscriptionId, bool isAuthenticated)
        {
            await storage.SetAsync(LoginKey, login);
            await storage.SetAsync(SubscriptionIdKey, subscriptionId);
            await storage.SetAsync(IsAuthenticatedKey, isAuthenticated);
        }

        public static async Task<bool> IsAuth(this ProtectedSessionStorage storage)
        {
            var result = await storage.GetAsync<bool>(IsAuthenticatedKey);
            return result.Success && result.Value;
        }

        public static async Task<string?> GetLogin(this ProtectedSessionStorage storage) => 
            await GetAuthValue<string?>(storage, LoginKey);

        public static async Task<long?> GetId(this ProtectedSessionStorage storage) => 
            await GetAuthValue<long?>(storage, SubscriptionIdKey);

        private static async Task<T?> GetAuthValue<T>(ProtectedSessionStorage storage, string key)
        {
            if (await IsAuth(storage))
            {
                var result = await storage.GetAsync<T>(key);
                return result.Success ? result.Value : default;
            }
            return default;
        }
    }
}
