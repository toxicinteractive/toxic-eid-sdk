using System.Net.Http.Json;
using System.Text.Json;
using Toxic.GrandId.Sdk.Features;

namespace Toxic.GrandId.Sdk;

public abstract partial class GrandIdClient
{
    private readonly HttpClient _httpClient;
    private readonly GrandIdOptions _options;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public GrandIdClient(HttpClient httpClient, GrandIdOptions options)
    {
        _httpClient = httpClient;
        _options = options;
    }

    public virtual partial Task<Features.FederatedLogin.FederatedLoginResponse> FederatedLogin(
        bool useGui,
        bool allowQrCode,
        bool allowFingerPrintAuth,
        bool allowFingerPrintSign,
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

        var responseObj = await httpResponse
            .Content
            .ReadFromJsonAsync<TResponse>(_jsonSerializerOptions);

        if (!httpResponse.IsSuccessStatusCode || responseObj == null || responseObj.IsError)
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
