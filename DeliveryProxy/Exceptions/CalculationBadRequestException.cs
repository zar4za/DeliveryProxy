namespace DeliveryProxy.Exceptions;

public class CalculationBadRequestException : DeliveryProxyException
{
    public CalculationBadRequestException() : base("Bad request") {}

    public CalculationBadRequestException(string message) : base(message) {}
}