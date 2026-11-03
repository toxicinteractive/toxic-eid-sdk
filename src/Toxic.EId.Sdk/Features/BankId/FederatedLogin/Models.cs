namespace Toxic.EId.Sdk.Features.BankId.FederatedLogin;

internal class EIdRequest : EIdRequestBase
{
    public bool Gui { get; set; }
    public bool Qr { get; set; }
    public bool AllowFingerprintAuth { get; set; }
    public bool AllowFingerprintSign { get; set; }
    public bool MobileBankId { get; set; }
    public bool DesktopBankId { get; set; }
    public bool ThisDevice { get; set; }
    public string? CallbackUrl { get; set; }
    public string? ReturnUrl { get; set; }
    public string? AuthMessage { get; set; }
    public string? UserVisibleData { get; set; }
    public string? UserNonVisibleData { get; set; }
    public string? UserVisibleDataFormat { get; set; }
}

internal class EIdResponse : EIdResponseBase
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
