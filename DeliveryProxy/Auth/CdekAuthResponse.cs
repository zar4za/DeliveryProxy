using System.Text.Json.Serialization;

namespace DeliveryProxy.Auth;

public class CdekAuthResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
}