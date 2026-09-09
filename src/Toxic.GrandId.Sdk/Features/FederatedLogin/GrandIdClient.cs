using Models = Toxic.GrandId.Sdk.Features.FederatedLogin;

namespace Toxic.GrandId.Sdk;

public abstract partial class GrandIdClient
{
    public virtual async partial Task<Models.FederatedLoginResponse> FederatedLogin(
        bool gui,
        bool qr,
        bool allowFingerPrintAuth,
        bool allowFingerPrintSign,
        bool mobileBankId,
        bool desktopBankId,
        bool thisDevice,
        string? callbackUrl,
        string? returnUrl,
        string? authMessage,
        string? userVisibleData,
        string? userNonVisibleData,
        string? userVisibleDataFormat)
    {
        var request = GetRequestObject<Models.GrandIdRequest>();

        request.Gui = gui;
        request.Qr = qr;
        request.AllowFingerprintAuth = allowFingerPrintAuth;
        request.AllowFingerprintSign = allowFingerPrintSign;
        request.MobileBankId = mobileBankId;
        request.DesktopBankId = desktopBankId;
        request.ThisDevice = thisDevice;
        request.CallbackUrl = callbackUrl;
        request.ReturnUrl = returnUrl;
        request.AuthMessage = authMessage;
        request.UserVisibleData = userVisibleData;
        request.UserNonVisibleData = userNonVisibleData;
        request.UserVisibleDataFormat = userVisibleDataFormat;

        var response = await Call<Models.GrandIdRequest, Models.GrandIdResponse>("FederatedLogin", request);
        return response.ToEntity();
    }
}
