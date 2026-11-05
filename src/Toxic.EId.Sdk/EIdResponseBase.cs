using System.Buffers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Toxic.EId.Sdk;

public abstract class EIdResponseBase
{
    public EIdErrorObject? ErrorObject { get; init; }
    public bool HasError => !string.IsNullOrWhiteSpace(ErrorObject?.Code);
}

public class EIdErrorObject
{
    [JsonConverter(typeof(NumberToStringConverter))]
    public string? Code { get; init; }
    public string? Message { get; init; }
}

public interface IFederatedLoginResponse
{
    string? SessionId { get; }
    string? QrCode { get; }
    string? RedirectUrl { get; }
    string? RefToken { get; }
}

public interface IGetSessionResponse
{
    bool IsCompleted { get; }
    bool IsPending { get; }
    bool HasError { get; }
    string? Status { get; }
    string? StatusMessage { get; }
    string? SessionId { get; }
    string? QrCode { get; }
    IUserAttributes? UserAttributes { get; init; }
}

public interface IUserAttributes
{
    string? PersonalNumber { get; }
    string? Name { get; }
    string? GivenName { get; }
    string? SurName { get; }
    string? IpAddress { get; }
    string? Signature { get; }
}

public sealed class NumberToStringConverter : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Number => Encoding.UTF8.GetString(reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan),
            _ => reader.GetString()
        };
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
