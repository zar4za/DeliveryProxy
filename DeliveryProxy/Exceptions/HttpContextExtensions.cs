namespace DeliveryProxy.Exceptions;

public static class HttpContextExtensions
{
    public static async Task ApplyExceptionResponse(this HttpContext context, ExceptionResponse response)
    {
        context.Response.StatusCode = (int)response.Status;
        await context.Response.WriteAsJsonAsync(response);
    }
}