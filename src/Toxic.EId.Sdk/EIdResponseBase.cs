namespace Toxic.EId.Sdk;

public abstract class EIdResponseBase
{
    public EIdErrorObject? ErrorObject { get; init; }
    public bool HasError => !string.IsNullOrWhiteSpace(ErrorObject?.Code);
}

public class EIdErrorObject
{
    public string? Code { get; init; }
    public string? Message { get; init; }
}
