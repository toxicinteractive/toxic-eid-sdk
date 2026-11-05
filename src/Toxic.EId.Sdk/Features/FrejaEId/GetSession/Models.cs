using System.Text.Json;
using System.Text.Json.Serialization;

namespace Toxic.EId.Sdk.Features.FrejaEId.GetSession;

internal class EIdRequest : EIdRequestBase
{
    public string? SessionId { get; set; }
}

internal class EIdResponse : EIdResponseBase
{
    public GrandIdObject? GrandIdObject { get; init; }

    public StatusObject? StatusObject { get; init; }
    public UserAttributes? UserAttributes { get; init; }

    public GetSessionResponse ToEntity() => 
        new()
        {
            Status = StatusObject?.Code,
            StatusMessage = StatusObject?.Message,
            SessionId = GrandIdObject?.SessionId,
            QrCode = GrandIdObject?.QrCode,
            UserAttributes = UserAttributes
        };
}

internal class GrandIdObject
{
    public string? Code { get; init; }
    public MessageObject? Message { get; init; }
    public string? SessionId { get; init; }
    public string? QrCode { get; init; }
}

internal class MessageObject
{
    public string? Status { get; init; }
    public string? HintCode { get; init; }
}

public class GetSessionResponse : IGetSessionResponse
{
    public bool IsCompleted => UserAttributes != null;
    public bool IsPending => Status is StatusCodes.Started or StatusCodes.Delivered;
    public bool HasError => Status is StatusCodes.Cancelled or StatusCodes.Expired;

    public string? Status { get; init; }
    public string? StatusMessage { get; init; }
    public string? SessionId { get; init; }
    public string? QrCode { get; init; }

    public string? UserName { get; init; }
    public IUserAttributes? UserAttributes { get; init; }
}

public class StatusObject
{
    public string? Code { get; init; }
    public string? Message { get; init; }
}

public class UserAttributes : IUserAttributes
{
    public string? PersonalNumber => RequestedAttributes?.Ssn?.Ssn ?? UserInfo?.Ssn;
    public string? Name => RequestedAttributes?.BasicUserInfo?.Name;
    public string? GivenName => RequestedAttributes?.BasicUserInfo?.Name;
    public string? SurName => RequestedAttributes?.BasicUserInfo?.SurName;
    
    public string? IpAddress { get; init; }
    public string? Signature { get; init; }

    public RequestedAttributes? RequestedAttributes { get; init; }

    [JsonConverter(typeof(UserInfoConverter))]
    public UserInfo? UserInfo { get; init; }
}

public class RequestedAttributes
{
    public BasicUserInfo? BasicUserInfo { get; init; }
    public UserInfo? Ssn { get; init; }
}

public class BasicUserInfo
{
    public string? Name { get; init; }
    public string? SurName { get; init; }
}

public class UserInfo
{
    public string? Ssn { get; init; }
    public string? Country { get; init; }
}

public sealed class UserInfoConverter : JsonConverter<UserInfo>
{
    public override UserInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        UserInfo? userInfo = null;
        var json = reader.GetString();

        if (!string.IsNullOrWhiteSpace(json))
        {
            // the userInfo field can be either a correct json object or a string "N/A"... 
            // this guards against deserialization exceptions
            try
            {
                userInfo = JsonSerializer.Deserialize<UserInfo>(json);
            } 
            catch
            {
                
            }
        }

        return userInfo;
    }

    public override void Write(Utf8JsonWriter writer, UserInfo value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
