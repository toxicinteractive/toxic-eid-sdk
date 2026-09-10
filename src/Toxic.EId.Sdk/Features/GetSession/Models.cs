namespace Toxic.EId.Sdk.Features.BankId.GetSession;

internal class EIdRequest : EIdRequestBase
{
    public string? SessionId { get; set; }
}

internal class EIdResponse : EIdResponseBase
{
    public GrandIdObject? GrandIdObject { get; init; }
    public UserAttributes? UserAttributes { get; init; }

    public GetSessionResponse ToEntity() => 
        new()
        {
            Status = GrandIdObject?.Message?.Status,
            HintCode = GrandIdObject?.Message?.HintCode,
            SessionId = GrandIdObject?.SessionId,
            QrCode = GrandIdObject?.QrCode,
            AutoStartToken = GrandIdObject?.AutoStartToken,
            UserAttributes = UserAttributes
        };
}

internal class GrandIdObject
{
    public string? Code { get; init; }
    public MessageObject? Message { get; init; }

    public string? SessionId { get; init; }
    public string? QrCode { get; init; }
    public string? AutoStartToken { get; init; }
}

internal class MessageObject
{
    public string? Status { get; init; }
    public string? HintCode { get; init; }
}

public class GetSessionResponse
{
    public bool IsCompleted => UserAttributes != null;
    public bool IsPending => Status == "pending";
    public bool HasError => Status == "failed";

    public string? Status { get; init; }
    public string? HintCode { get; init; }

    public string? SessionId { get; init; }
    public string? QrCode { get; init; }
    public string? AutoStartToken { get; init; }

    public UserAttributes? UserAttributes { get; init; }
}

public class UserAttributes
{
    public string? PersonalNumber { get; init; }
    public string? Name { get; init; }
    public string? GivenName { get; init; }
    public string? SurName { get; init; }
    public string? IpAddress { get; init; }
    public string? Signature { get; init; }
}
