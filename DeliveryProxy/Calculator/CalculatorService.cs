using System.Net;
using DeliveryProxy.Calculator.Cdek;
using DeliveryProxy.Exceptions;

namespace DeliveryProxy.Calculator;

public class CalculatorService
{
    private const string CalculatorEndpoint = "calculator/tariff";

    private readonly HttpClient _httpClient;

    public CalculatorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CdekCalcResponse> GetShipmentPriceAsync(CdekCalcRequest request, CancellationToken cancellation)
    {
        var response = await _httpClient.PostAsJsonAsync(CalculatorEndpoint, request, cancellation);

        if (response.IsSuccessStatusCode)
        {
            var calc = await response.Content.ReadFromJsonAsync<CdekCalcResponse>(cancellationToken: cancellation);
            return calc!;
        }

        if (response.StatusCode != HttpStatusCode.BadRequest)
            throw new ExternalApiException($"{_httpClient.BaseAddress}/{CalculatorEndpoint}");

        throw new CalculationBadRequestException();
    }
}