using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Authentication;
using TechReserveSystem.Shared.Exceptions.Resources;

namespace TechReserveSystem.Infrastructure.Security.Authentication
{
    public static class TokenValidationFailureHandler
    {
        public static Task ProcessResponseAsync()
        {
            throw new AuthenticationException(ResourceMessagesException.INVALID_JWT);
        }
    }
}