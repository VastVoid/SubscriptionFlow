namespace Application.Models.Auth;

public record AuthResult(bool Success, long SubscriptionId, string Login);