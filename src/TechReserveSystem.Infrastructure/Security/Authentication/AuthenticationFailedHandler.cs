using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Resources;


namespace TechReserveSystem.Infrastructure.Security.Authentication
{
    public static class AuthenticationFailedHandler
    {
        public static Task ProcessResponseAsync(AuthenticationFailedContext context)
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            var response = JsonSerializer.Serialize(new { erro = ResourceAppMessages.GetExceptionMessage(AuthMessagesExceptions.INVALID_JWT) });
            if (context.Exception is SecurityTokenExpiredException)
            {
                response = JsonSerializer.Serialize(new { erro = ResourceAppMessages.GetExceptionMessage(AuthMessagesExceptions.TOKEN_EXPIRED) });
                return context.Response.WriteAsync(response);
            }
            return context.Response.WriteAsync(response);
        }
    }
}