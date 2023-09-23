using System.Text.Json.Serialization;

namespace DeliveryProxy.Calculator.Cdek;

public class CdekCalcRequest
{
    [JsonPropertyName("from_location")]
    public CdekLocation FromLocation { get; set; }

    [JsonPropertyName("to_location")]
    public CdekLocation ToLocation { get; set; }

    public CdekPackage[] Packages { get; set; }
}