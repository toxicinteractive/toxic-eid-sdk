namespace Toxic.GrandId.Sdk;

public abstract class GrandIdRequestBase
{
    public string? ApiKey { get; init; }
    public string? AuthenticateServiceKey { get; init; }

    public Dictionary<string, string?> ToFormParameters()
    {
        string toCamelCase(string str) => $"{str[0].ToString().ToLowerInvariant()}{str[1..]}";
        string? valueToString(object? value) => value is bool b ? b.ToString().ToLowerInvariant() : value?.ToString();

        return GetType()
            .GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
            .ToDictionary(
                x => toCamelCase(x.Name), 
                y => valueToString(y.GetValue(this)));
    }
}
