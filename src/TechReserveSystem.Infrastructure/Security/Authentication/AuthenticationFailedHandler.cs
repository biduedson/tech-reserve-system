using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
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
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                var response = JsonSerializer.Serialize(new { erro = ResourceMessagesException.TOKEN_EXPIRED });
                return context.Response.WriteAsync(response);
            }

            var responseInvalidToken = JsonSerializer.Serialize(new { erro = ResourceMessagesException.TOKEN_EXPIRED });
            return context.Response.WriteAsync(responseInvalidToken);
        }
    }
}