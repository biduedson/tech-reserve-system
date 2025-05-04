using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TechReserveSystem.Infrastructure.Configuration;

namespace TechReserveSystem.Infrastructure.Security.Authentication
{
    public static class AddTokenValidationService
    {
        public static void ConfigAuthentication(IServiceCollection services, IOptions<JwtSettings> config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = TokenParameters.Parameters(config);
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context => TokenValidationFailureHandler.ProcessResponseAsync(),
                    OnForbidden = context => ForbiddenResponseHandler.ProcessResponseAsync(context),
                    OnAuthenticationFailed = context => AuthenticationFailedHandler.ProcessResponseAsync(context),

                };
            });
        }
    }
}