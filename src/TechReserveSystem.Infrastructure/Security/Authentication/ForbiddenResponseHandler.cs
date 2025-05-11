using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Infrastructure.Security.Authentication
{
    public static class ForbiddenResponseHandler
    {
        public static Task ProcessResponseAsync(ForbiddenContext context)
        {
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";
            var response = JsonSerializer.Serialize(new { erro = ResourceAppMessages.GetExceptionMessage(AuthMessagesExceptions.AUTHORIZATION_ERROR) });
            return context.Response.WriteAsync(response);
        }
    }
}