namespace Toxic.EId.Sdk.Features.FrejaEId.Logout;

internal class EIdRequest : EIdRequestBase
{
    public string? SessionId { get; set; }
}

internal class EIdResponse : EIdResponseBase
{
    public LogoutResponse ToEntity() => new();
}

public class LogoutResponse
{
    
}
