namespace Toxic.EId.Sdk.SampleApp.Models;

public class CreateSessionRequest
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
