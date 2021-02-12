using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace RealWorldOne.UserManagement.Api.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
        }
        
        public async Task Invoke(HttpContext context)
        {
            LogRequest(context);
            await _next(context);
            LogResponse(context);  
        }
        
        private void LogRequest(HttpContext context)
        {
            _logger.LogInformation(
                "Http Request Information: {Environment} " +
                "Schema:{Scheme} " +
                "Host: {Host} " +
                "Path: {Path} " +
                "QueryString: {QueryString}",
                Environment.MachineName,
                context.Request.Scheme,
                context.Request.Host,
                context.Request.Path,
                context.Request.QueryString);
        }
        
        private void LogResponse(HttpContext context)
        {
            _logger.LogInformation(
                "Http Request Information: {Environment} " +
                "Schema:{Scheme} " +
                "Host: {Host} " +
                "Path: {Path} " +
                "QueryString: {QueryString}",
                Environment.MachineName,
                context.Request.Scheme,
                context.Request.Host,
                context.Request.Path,
                context.Request.QueryString);
        }
    }
}