using Microsoft.AspNetCore.Builder;
using RealWorldOne.UserManagement.Api.Middleware;

namespace RealWorldOne.UserManagement.Api.Extensions
{
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}