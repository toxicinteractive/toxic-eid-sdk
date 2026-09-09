namespace Toxic.GrandId.Sdk;

public abstract class GrandIdResponseBase
{
    public GrandIdErrorObject? ErrorObject { get; init; }
    public bool HasError => !string.IsNullOrWhiteSpace(ErrorObject?.Code);
}

public class GrandIdErrorObject
{
    public string? Code { get; init; }
    public string? Message { get; init; }
}
