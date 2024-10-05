using Application.Models.Auth;

namespace Application.Services.Abstractions;

public interface IAuthService
{
    Task<AuthResult> LogIn(string login);
}
