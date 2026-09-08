namespace Toxic.GrandId.Sdk.Features.GetSession;

internal class GrandIdRequest : GrandIdRequestBase
{
    public string? SessionId { get; set; }
}

internal class GrandIdResponse : GrandIdResponseBase
{
    public string? SessionId { get; init; }
    public string? QrCode { get; init; }

    public GetSessionResponse ToEntity() => 
        new()
        {
            SessionId = SessionId,
            QrCode = QrCode
        };
}

public class GetSessionResponse
{
    public string? SessionId { get; init; }
    public string? QrCode { get; init; }
}
