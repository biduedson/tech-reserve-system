using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TechReserveSystem.Application.Interfaces.Services.Authentication;
using TechReserveSystem.Infrastructure.Configuration;

namespace TechReserveSystem.Infrastructure.Services.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _config;

        public TokenService(IOptions<JwtSettings> config)
        {
            _config = config.Value;
        }

        public string GenerateToken(Guid userId, string email, string role)
        {
            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
               new Claim(ClaimTypes.Email, email),
               new Claim(ClaimTypes.Role, role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config.Issuer,
                audience: _config.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_config.ExpiresInHours),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public DateTime ExperiOn(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            return jwtToken.ValidTo;
        }
    }
}