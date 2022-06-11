using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;

namespace VehicleRepairLog.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotAuthenticatedException notAuthenticatedException)
            {
                _logger.LogError(notAuthenticatedException, "");

                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(notAuthenticatedException.Message);
            }
            catch (UnauthorizedException unauthorizedException)
            {
                _logger.LogError(unauthorizedException, "");

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync(unauthorizedException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                _logger.LogError(notFoundException, "");

                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "");

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(exception.Message);
            }
        }
    }
}
