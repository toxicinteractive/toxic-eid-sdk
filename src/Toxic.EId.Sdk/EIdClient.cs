using System.Text.Json;
using Microsoft.Extensions.Logging;
using Toxic.EId.Sdk.Features;

namespace Toxic.EId.Sdk;

public abstract partial class EIdClient
{
    private readonly HttpClient _httpClient;
    private readonly EIdOptions _options;
    private readonly ILogger<EIdClient> _logger;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public EIdClient(HttpClient httpClient, EIdOptions options, ILogger<EIdClient> logger)
    {
        _httpClient = httpClient;
        _options = options;
        _logger = logger;
    }

    protected async Task<TResponse> Call<TRequest, TResponse>(string endpoint, TRequest request) 
        where TRequest : EIdRequestBase
        where TResponse : EIdResponseBase
    {
        var httpResponse = await _httpClient.PostAsync(
            endpoint, 
            new FormUrlEncodedContent(request.ToFormParameters()));

        var contents = await httpResponse.Content.ReadAsStringAsync();
        _logger.LogDebug("Raw response from {Endpoint}: {Message}", endpoint, contents);
        
        var responseObj = JsonSerializer.Deserialize<TResponse>(contents, _jsonSerializerOptions);

        if (!httpResponse.IsSuccessStatusCode || responseObj == null || responseObj.HasError)
        {
            throw new EIdException(
                responseObj?.ErrorObject?.Message ?? "Unknown error", 
                responseObj?.ErrorObject?.Code ?? httpResponse.StatusCode.ToString());
        }

        return responseObj;
    }

    protected T GetRequestObject<T>() where T : EIdRequestBase, new()
    {
        return new T()
        {
            ApiKey = _options.ApiKey,
            AuthenticateServiceKey = _options.ServiceKey
        };
    }
}
