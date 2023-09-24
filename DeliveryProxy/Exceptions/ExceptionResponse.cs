using System.Net;

namespace DeliveryProxy.Exceptions;

public class ExceptionResponse
{
    public HttpStatusCode Status { get; init; }

    public string? ErrorName { get; init; } 

    public required string Message { get; init; }
}