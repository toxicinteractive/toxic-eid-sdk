using Models = Toxic.EId.Sdk.Features.BankId.FederatedLogin;

namespace Toxic.EId.Sdk.Features.BankId;

public partial class BankIdClient
{
    public virtual async partial Task<Models.FederatedLoginResponse> FederatedLogin(
        Func<EIdOptions, string?> serviceKeySelector,
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
        var serviceKey = serviceKeySelector?.Invoke(Options) ?? 
            throw new InvalidOperationException("Invalid BankId service key");
            
        var request = GetRequestObject<Models.EIdRequest>(serviceKey);

        request.Gui = gui;
        request.Qr = qr;
        request.AllowFingerprintAuth = allowFingerPrintAuth;
        request.AllowFingerprintSign = allowFingerPrintSign;
        request.MobileBankId = mobileBankId;
        request.DesktopBankId = desktopBankId;
        request.ThisDevice = thisDevice;
        request.CallbackUrl = callbackUrl;
        request.ReturnUrl = returnUrl;
        request.AuthMessage = authMessage?.ToBase64();
        request.UserVisibleData = userVisibleData?.ToBase64();
        request.UserNonVisibleData = userNonVisibleData?.ToBase64();
        request.UserVisibleDataFormat = userVisibleDataFormat;

        var response = await Call<Models.EIdRequest, Models.EIdResponse>("FederatedLogin", request);
        return response.ToEntity();
    }
}
