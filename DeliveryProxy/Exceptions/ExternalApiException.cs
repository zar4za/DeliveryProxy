namespace DeliveryProxy.Exceptions;

public class ExternalApiException : DeliveryProxyException
{
    public ExternalApiException(string uri) : base($"Failed to access '{uri}'") {}
}