using System.Net.Http.Headers;
using Microsoft.Extensions.Options;

namespace DeliveryProxy.Auth;

public class AuthorizedHttpClientFactory : IHttpClientFactory
{
    private readonly HttpClient _httpClient = new();
    private DateTime _expiration = DateTime.UtcNow;
    private readonly FormUrlEncodedContent _oAuthCredsContent;

    public AuthorizedHttpClientFactory(IOptions<CdekAuthOptions> authOptions)
    {
        var creds = new Dictionary<string, string>
        {
            {"grant_type", "client_credentials"},
            {"client_id", authOptions.Value.ClientId},
            {"client_secret", authOptions.Value.ClientSecret}
        };

        _oAuthCredsContent = new FormUrlEncodedContent(creds);
        _httpClient.BaseAddress = new Uri("https://api.edu.cdek.ru/v2/");
    }

    public HttpClient CreateClient(string name)
    {
        if (_expiration < DateTime.UtcNow)
        {
            RefreshBearerToken().Wait();
        }

        return _httpClient;
    }

    private async Task RefreshBearerToken()
    {
        var result = await _httpClient.PostAsync("oauth/token?parameters", _oAuthCredsContent);
        var authResponse = await result.Content.ReadFromJsonAsync<CdekAuthResponse>();
        _expiration = DateTime.UtcNow.AddSeconds(authResponse!.ExpiresIn);
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", authResponse!.AccessToken);
    }
}