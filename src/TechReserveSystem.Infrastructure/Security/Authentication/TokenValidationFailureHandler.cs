using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Resources;


namespace TechReserveSystem.Infrastructure.Security.Authentication
{
    public static class TokenValidationFailureHandler
    {
        public static Task ProcessResponseAsync(JwtBearerChallengeContext context)
        {
            context.HandleResponse();
            context.Response.StatusCode = 401; // Não autorizado
            context.Response.ContentType = "application/json";
            var response = JsonSerializer.Serialize(new { erro = ResourceAppMessages.GetExceptionMessage(AuthMessagesExceptions.INVALID_JWT) });
            return context.Response.WriteAsync(response);
        }
    }
}
