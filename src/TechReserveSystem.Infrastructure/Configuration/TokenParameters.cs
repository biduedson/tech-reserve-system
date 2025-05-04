using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace TechReserveSystem.Infrastructure.Configuration
{
    public static class TokenParameters
    {
        public static TokenValidationParameters Parameters(IOptions<JwtSettings> config)
        {
            var parameters = config.Value;
            return new TokenValidationParameters
            {

                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = parameters.Issuer,
                ValidAudience = parameters.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(parameters.Key!)),
                RoleClaimType = "role"
            };
        }
    }
}