namespace Toxic.EId.Sdk.Features;

public class EIdException : Exception
{
    public string? Code { get; init; }

    public EIdException(string message, string? code = null, Exception? innerException = null) : base(message, innerException)
    {
        Code = code;
    }
}
