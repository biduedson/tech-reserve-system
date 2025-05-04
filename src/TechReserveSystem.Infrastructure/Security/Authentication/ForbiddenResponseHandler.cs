using Microsoft.AspNetCore.Authentication.JwtBearer;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Authorization;
using TechReserveSystem.Shared.Exceptions.Resources;

namespace TechReserveSystem.Infrastructure.Security.Authentication
{
    public static class ForbiddenResponseHandler
    {
        public static Task ProcessResponseAsync(ForbiddenContext context)
        {
            throw new AuthorizationException(ResourceMessagesException.AUTHORIZATION_ERROR);
        }
    }
}