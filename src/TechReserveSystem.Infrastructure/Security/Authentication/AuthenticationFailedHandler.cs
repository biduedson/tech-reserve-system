using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Authentication;
using TechReserveSystem.Shared.Exceptions.Resources;

namespace TechReserveSystem.Infrastructure.Security.Authentication
{
    public static class AuthenticationFailedHandler
    {
        public static Task ProcessResponseAsync(AuthenticationFailedContext context)
        {
            if (context.Exception is SecurityTokenExpiredException)
            {
                throw new AuthenticationException(ResourceMessagesException.TOKEN_EXPIRED);
            }

            throw new AuthenticationException(ResourceMessagesException.INVALID_JWT);
        }
    }
}