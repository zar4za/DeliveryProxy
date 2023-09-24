using System.Net;
using Mapster;

namespace DeliveryProxy.Exceptions;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (DeliveryProxyException ex)
        {
            var response = ex.Adapt<ExceptionResponse>();
            await context.ApplyExceptionResponse(response);
        }
        catch
        {
            var response = new ExceptionResponse
            {
                Status = HttpStatusCode.InternalServerError,
                Message = "An unexpected error has occurred",
                ErrorName = "Unexpected error"
            };
            await context.ApplyExceptionResponse(response);
        }
    }
}