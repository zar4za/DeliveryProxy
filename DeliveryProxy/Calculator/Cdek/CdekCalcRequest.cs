using System.Text.Json.Serialization;

namespace DeliveryProxy.Calculator.Cdek;

public class CdekCalcRequest
{
    // we use door to door delivery (by courier) without any additional services 
    private const int DefaultTariffCode = 139;


    [JsonPropertyName("tariff_code")]
    public int TariffCode { get; } = DefaultTariffCode;

    [JsonPropertyName("from_location")]
    public CdekLocation FromLocation { get; set; }

    [JsonPropertyName("to_location")]
    public CdekLocation ToLocation { get; set; }

    [JsonPropertyName("packages")]
    public CdekPackage[] Packages { get; set; }
}