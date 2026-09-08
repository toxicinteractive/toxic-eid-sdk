namespace Toxic.GrandId.Sdk.Features;

public class GrandIdException : Exception
{
    public string? Code { get; init; }

    public GrandIdException(string message, string? code = null, Exception? innerException = null) : base(message, innerException)
    {
        Code = code;
    }
}
