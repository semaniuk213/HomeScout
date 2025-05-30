using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HomeScout.Auth.Models;
using HomeScout.Auth.Options;
using HomeScout.DAL.Entities;
using HomeScout.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HomeScout.Auth.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtSettings _jwtSettings;

        public AccountService(UserManager<User> userManager, IUnitOfWork unitOfWork, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthResponseModel> RegisterAsync(RegisterModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, "User");

            return await GenerateTokensAsync(user);
        }

        public async Task<AuthResponseModel> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                throw new UnauthorizedAccessException("Invalid credentials");

            return await GenerateTokensAsync(user);
        }

        public async Task<AuthResponseModel> RefreshTokenAsync(string refreshToken)
        {
            var user = await _unitOfWork.UserRepository.GetByRefreshTokenAsync(refreshToken);
            if (user == null)
                throw new SecurityTokenException("Invalid refresh token");

            var token = user.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken);
            if (token == null || !token.IsActive)
                throw new SecurityTokenException("Expired or revoked refresh token");

            token.Revoked = DateTime.UtcNow;

            return await GenerateTokensAsync(user);
        }

        public async Task RevokeTokenAsync(string refreshToken)
        {
            var user = await _unitOfWork.UserRepository.GetByRefreshTokenAsync(refreshToken);
            if (user == null) return;

            var token = user.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken);
            if (token != null)
            {
                token.Revoked = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);
            }
        }

        private async Task<AuthResponseModel> GenerateTokensAsync(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresIn),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var accessToken = tokenHandler.WriteToken(token);

            var refreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);

            return new AuthResponseModel
            {
                Token = accessToken,
                RefreshToken = refreshToken.Token,
                Expiration = token.ValidTo
            };
        }

        private RefreshToken GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7)
            };
        }
    }
}
