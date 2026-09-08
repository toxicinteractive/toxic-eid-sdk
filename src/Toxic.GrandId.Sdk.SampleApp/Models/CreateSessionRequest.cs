namespace Toxic.GrandId.Sdk.SampleApp.Models;

public class CreateSessionRequest
{
    public bool UseGui { get; set; }
    public bool AllowQr { get; set; }
    public bool AllowFingerprintAuth { get; set; }
    public bool AllowFingerprintSign { get; set; }
    public string? CallbackUrl { get; set; }
    public string? ReturnUrl { get; set; }
    public string? AuthMessage { get; set; }
    public string? UserVisibleData { get; set; }
    public string? UserNonVisibleData { get; set; }
    public string? UserVisibleDataFormat { get; set; }
}
