namespace Toxic.GrandId.Sdk.Features.Logout;

internal class GrandIdRequest : GrandIdRequestBase
{
    public string? SessionId { get; set; }
}

internal class GrandIdResponse : GrandIdResponseBase
{
    public LogoutResponse ToEntity() => new();
}

public class LogoutResponse
{
    
}
