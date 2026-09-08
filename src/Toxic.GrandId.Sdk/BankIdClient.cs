using Microsoft.Extensions.Options;

namespace Toxic.GrandId.Sdk;

public class BankIdClient : GrandIdClient
{
    public BankIdClient(HttpClient httpClient, IOptions<GrandIdOptions> options) 
        : base(httpClient, options.Value)
    {
        
    }
}
