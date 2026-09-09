using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Toxic.GrandId.Sdk;

public class BankIdClient : GrandIdClient
{
    public BankIdClient(HttpClient httpClient, IOptions<GrandIdOptions> options, ILogger<BankIdClient> logger) 
        : base(httpClient, options.Value, logger)
    {
        
    }
}
