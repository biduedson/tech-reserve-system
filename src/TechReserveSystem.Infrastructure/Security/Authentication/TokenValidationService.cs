using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TechReserveSystem.Infrastructure.Configuration;

namespace TechReserveSystem.Infrastructure.Security.Authentication
{
    public static class AddTokenValidationService
    {
        public static void ConfigAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = TokenParameters.Parameters(configuration);
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context => TokenValidationFailureHandler.ProcessResponseAsync(context),
                    OnForbidden = context => ForbiddenResponseHandler.ProcessResponseAsync(context),
                    OnAuthenticationFailed = context => AuthenticationFailedHandler.ProcessResponseAsync(context),

                };
            });
        }
    }
}