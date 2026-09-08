namespace Toxic.GrandId.Sdk.Features.FederatedLogin;

internal class GrandIdRequest : GrandIdRequestBase
{
    public bool Gui { get; set; }
    public bool Qr { get; set; }
    public bool AllowFingerprintAuth { get; set; }
    public bool AllowFingerprintSign { get; set; }
    public string? CallbackUrl { get; set; }
    public string? ReturnUrl { get; set; }
    public string? AuthMessage { get; set; }
    public string? UserVisibleData { get; set; }
    public string? UserNonVisibleData { get; set; }
    public string? UserVisibleDataFormat { get; set; }
}

internal class GrandIdResponse : GrandIdResponseBase
{
    public string? SessionId { get; init; }
    public string? RedirectUrl { get; init; }
    public string? AutoStartToken { get; init; }
    public string? QrCode { get; init; }

    public FederatedLoginResponse ToEntity() => 
        new()
        {
            SessionId = SessionId,
            QrCode = QrCode,
            RedirectUrl = RedirectUrl,
            AutoStartToken = AutoStartToken
        };
}

public class FederatedLoginResponse
{
    public string? SessionId { get; init; }
    public string? QrCode { get; init; }
    public string? RedirectUrl { get; init; }
    public string? AutoStartToken { get; init; }
}
