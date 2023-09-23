using System.Text.Json.Serialization;

namespace DeliveryProxy.Calculator.Cdek;

public class CdekCalcResponse
{
    [JsonPropertyName("total_sum")]
    public decimal TotalPrice { get; set; }
}