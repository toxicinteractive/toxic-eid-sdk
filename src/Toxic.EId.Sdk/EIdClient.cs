using System.Text.Json;
using Microsoft.Extensions.Logging;
using Toxic.EId.Sdk.Features;

namespace Toxic.EId.Sdk;

public abstract partial class EIdClient
{
    protected EIdOptions Options { get; }
    protected ILogger<EIdClient> Logger { get; }

    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public EIdClient(HttpClient httpClient, EIdOptions options, ILogger<EIdClient> logger)
    {
        _httpClient = httpClient;
        Options = options;
        Logger = logger;
    }

    protected async Task<TResponse> Call<TRequest, TResponse>(string endpoint, TRequest request)
        where TRequest : EIdRequestBase
        where TResponse : EIdResponseBase
    {
        if (Logger.IsEnabled(LogLevel.Debug))
        {
            Logger.LogDebug("Raw request to {Host}{Endpoint}: {Request}", _httpClient.BaseAddress, endpoint, JsonSerializer.Serialize(request));
        }

        var httpResponse = await _httpClient.PostAsync(
            endpoint,
            new FormUrlEncodedContent(request.ToFormParameters()));

        var contents = await httpResponse.Content.ReadAsStringAsync();

        if (Logger.IsEnabled(LogLevel.Debug))
        {
            Logger.LogDebug("Raw response from {Host}{Endpoint}: {Message}", _httpClient.BaseAddress, endpoint, contents);
        }

        var responseObj = JsonSerializer.Deserialize<TResponse>(contents, _jsonSerializerOptions);

        if (!httpResponse.IsSuccessStatusCode || responseObj == null || responseObj.HasError)
        {
            throw new EIdException(
                responseObj?.ErrorObject?.Message ?? "Unknown error",
                responseObj?.ErrorObject?.Code ?? httpResponse.StatusCode.ToString());
        }

        return responseObj;
    }

    protected T GetRequestObject<T>(string serviceKey) where T : EIdRequestBase, new()
    {
        return new T()
        {
            ApiKey = Options.ApiKey,
            AuthenticateServiceKey = serviceKey
        };
    }
}
