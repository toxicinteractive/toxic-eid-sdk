using System.ComponentModel.DataAnnotations;

namespace Toxic.EId.Sdk;

public class EIdOptions
{
    public string EuEndpoint => IsTest ? "https://client-test.grandid.com" : "https://client.grandid.com";
    public string SeEndpoint => IsTest ? "https://client.test.e-identitet.se" : "https://client.e-identitet.se";
    public string Endpoint => UseEuEndpoint ? EuEndpoint : SeEndpoint;

    [Required]
    public string? ApiKey { get; set; }
    public bool IsTest { get; set; }

    /// <summary>
    /// Set to true to log the complete raw request and response data to and from the API (at the debug log level).
    /// This includes any sensitive information such as API keys and personal numbers!
    /// </summary>
    public bool LogRequestData { get; set; }

    /// <summary>
    /// Set to true to use EU endpoint instead of SE endpoint.
    /// </summary>
    public bool UseEuEndpoint { get; set; }

    public BankIdOptions BankId { get; set; } = new();
    public FrejaEIdOptions FrejaEId { get; set; } = new();
}

public class BankIdOptions
{
    [Required]
    public string? AuthServiceKey { get; set; }
    public string? SignServiceKey { get; set; }
}

public class FrejaEIdOptions
{
    [Required]
    public string? AuthServiceKey { get; set; }
    public string? SignServiceKey { get; set; }
}
