using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Authentication;
using TechReserveSystem.Shared.Exceptions.Resources;

namespace TechReserveSystem.Infrastructure.Security.Authentication
{
    public static class TokenValidationFailureHandler
    {
        public static Task ProcessResponseAsync(JwtBearerChallengeContext context)
        {
            context.HandleResponse();
            context.Response.StatusCode = 401; // Não autorizado
            context.Response.ContentType = "application/json";

            var response = JsonSerializer.Serialize(new { erro = ResourceMessagesException.INVALID_JWT });
            return context.Response.WriteAsync(response);
        }
    }
}
