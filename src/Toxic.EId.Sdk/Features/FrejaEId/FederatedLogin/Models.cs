namespace Toxic.EId.Sdk.Features.FrejaEId.FederatedLogin;

internal class EIdRequest : EIdRequestBase
{
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

internal class EIdResponse : EIdResponseBase
{
    public string? SessionId { get; init; }
    public string? RedirectUrl { get; init; }
    public string? Qr { get; init; }
    public string? AuthRef { get; init; }

    public FederatedLoginResponse ToEntity() => 
        new()
        {
            SessionId = SessionId,
            QrCode = Qr,
            RedirectUrl = RedirectUrl,
            RefToken = AuthRef
        };
}

public class FederatedLoginResponse : IFederatedLoginResponse
{
    public string? SessionId { get; init; }
    public string? QrCode { get; init; }
    public string? RedirectUrl { get; init; }
    public string? RefToken { get; init; }
}
