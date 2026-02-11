using _27sep.Responses.Auth;

namespace _27sep.Services
{
    public interface ITokenService
    {
        string CreateAccessToken(AuthResponse authResponse);
    }
}
