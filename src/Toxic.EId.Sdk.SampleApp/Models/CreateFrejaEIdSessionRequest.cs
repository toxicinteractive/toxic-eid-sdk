namespace Toxic.EId.Sdk.SampleApp.Models;

public class CreateFrejaEIdSessionRequest
{
    public bool IsSigning { get; set; }
    
    public bool Gui { get; set; }
    public bool Qr { get; set; }
    public bool ThisDevice { get; set; }
    public bool OtherDevice { get; set; }
    public bool DeviceChoice { get; set; }
    public bool EnableAutoLaunch { get; set; }
    public string? UserInfoType { get; set; }
    public string? MinRegistrationLevel { get; set; }
    public string? PersonalNumber { get; set; }
    public string? CallbackUrl { get; set; }
    public string? UserVisibleData { get; set; }
    public string? UserNonVisibleData { get; set; }
    public string? Title { get; set; }
    public string? UserInfo { get; set; }
    public string? CountryCode { get; set; }
    public string? PushNotification { get; set; }
    public string? CustomerUrl { get; set; }
}
