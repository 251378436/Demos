using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace FirstAPI.Outbound;

public class Auth0HttpHandler : DelegatingHandler
{
    IOptions<OutboundAuth> options;
    public Auth0HttpHandler(IOptions<OutboundAuth> options)
    {
        this.options = options;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await GetToken(cancellationToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<TokenData> GetToken(CancellationToken cancellationToken)
    {
        // TODO: add this token into memory cache
        var outboundAuth = options.Value;
        var client = new AuthenticationApiClient(outboundAuth.Domain);

        var credentialsTokenRequest = new ClientCredentialsTokenRequest
        {
            ClientId = outboundAuth.ClientId,
            ClientSecret = outboundAuth.ClientSecret,
            Audience = outboundAuth.Audience
        };

        var accessTokenResponse = await client.GetTokenAsync(credentialsTokenRequest, cancellationToken);
        var tokenData = MapToTokenData(accessTokenResponse);
        return tokenData;
    }

    private TokenData MapToTokenData(AccessTokenResponse accessTokenResponse)
    {
        var tokenData = new TokenData();
        tokenData.AccessToken = accessTokenResponse.AccessToken;
        tokenData.ExpiresIn = accessTokenResponse.ExpiresIn;
        tokenData.TokenType = accessTokenResponse.TokenType;
        return tokenData;
    }
}
