using System.Net;
using System.Text.Json.Nodes;
using DeliveryProxy.Calculator.Cdek;
using DeliveryProxy.Exceptions;

namespace DeliveryProxy.Calculator;

public class CalculatorService
{
    private const string CalculatorEndpoint = "calculator/tariff";

    private readonly HttpClient _httpClient;

    public CalculatorService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
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

        var error = await response.Content.ReadFromJsonAsync<JsonObject>(cancellationToken: cancellation);
        throw new CalculationBadRequestException(error?["errors"]?[0]?["message"]?.GetValue<string>() ?? string.Empty);
    }
}