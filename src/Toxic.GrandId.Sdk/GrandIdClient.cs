using System.Text.Json;
using Microsoft.Extensions.Logging;
using Toxic.GrandId.Sdk.Features;

namespace Toxic.GrandId.Sdk;

public abstract partial class GrandIdClient
{
    private readonly HttpClient _httpClient;
    private readonly GrandIdOptions _options;
    private readonly ILogger<GrandIdClient> _logger;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public GrandIdClient(HttpClient httpClient, GrandIdOptions options, ILogger<GrandIdClient> logger)
    {
        _httpClient = httpClient;
        _options = options;
        _logger = logger;
    }

    public virtual partial Task<Features.FederatedLogin.FederatedLoginResponse> FederatedLogin(
        bool gui,
        bool qr,
        bool allowFingerPrintAuth,
        bool allowFingerPrintSign,
        bool mobileBankId,
        bool desktopBankId,
        bool thisDevice,
        string? callbackUrl = null,
        string? returnUrl = null,
        string? authMessage = null,
        string? userVisibleData = null,
        string? userNonVisibleData = null,
        string? userVisibleDataFormat = null);
    public virtual partial Task<Features.GetSession.GetSessionResponse> GetSession(string sessionId);
    public virtual partial Task<Features.Logout.LogoutResponse> Logout(string sessionId);

    protected async Task<TResponse> Call<TRequest, TResponse>(string endpoint, TRequest request) 
        where TRequest : GrandIdRequestBase
        where TResponse : GrandIdResponseBase
    {
        var httpResponse = await _httpClient.PostAsync(
            endpoint, 
            new FormUrlEncodedContent(request.ToFormParameters()));

        var contents = await httpResponse.Content.ReadAsStringAsync();
        _logger.LogDebug("Raw response from {Endpoint}: {Message}", endpoint, contents);
        
        var responseObj = JsonSerializer.Deserialize<TResponse>(contents, _jsonSerializerOptions);

        if (!httpResponse.IsSuccessStatusCode || responseObj == null || responseObj.HasError)
        {
            throw new GrandIdException(
                responseObj?.ErrorObject?.Message ?? "Unknown error", 
                responseObj?.ErrorObject?.Code ?? httpResponse.StatusCode.ToString());
        }

        return responseObj;
    }

    protected T GetRequestObject<T>() where T : GrandIdRequestBase, new()
    {
        return new T()
        {
            ApiKey = _options.ApiKey,
            AuthenticateServiceKey = _options.ServiceKey
        };
    }
}
