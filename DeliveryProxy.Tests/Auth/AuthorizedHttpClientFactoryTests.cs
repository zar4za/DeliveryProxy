using System.Net;
using DeliveryProxy.Auth;
using Microsoft.Extensions.Options;

namespace DeliveryProxy.Tests.Auth;

public class AuthorizedHttpClientFactoryTests
{
    [Fact]
    public async Task CreateFactory_CorrectCreds_ShouldAuthorizeApi()
    {
        // sample credentials no need to worry about safety
        var storedConfig = new CdekAuthOptions
        {
            ClientId = "EMscd6r9JnFiQ3bLoyjJY6eM78JrJceI",
            ClientSecret = "PjLZkKBHEiLK3YsjtNrt3TGNG0ahs3kG"
        };
        var options = Options.Create(storedConfig);
        var factory = new AuthorizedHttpClientFactory(options);

        var httpClient = factory.CreateClient("cdek");

        var result = await httpClient.GetAsync("https://api.edu.cdek.ru/v2/location/postalcodes");

        Assert.NotEqual(HttpStatusCode.Unauthorized, result.StatusCode);
    }
}