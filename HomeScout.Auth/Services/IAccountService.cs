using HomeScout.Auth.Models;

namespace HomeScout.Auth.Services
{
    public interface IAccountService
    {
        Task<AuthResponseModel> RegisterAsync(RegisterModel model);
        Task<AuthResponseModel> LoginAsync(LoginModel model);
        Task<AuthResponseModel> RefreshTokenAsync(string refreshToken);
        Task RevokeTokenAsync(string refreshToken);
    }
}
