using Models = Toxic.GrandId.Sdk.Features.FederatedLogin;

namespace Toxic.GrandId.Sdk;

public abstract partial class GrandIdClient
{
    public virtual async partial Task<Models.FederatedLoginResponse> FederatedLogin(
        bool useGui,
        bool allowQrCode,
        bool allowFingerPrintAuth,
        bool allowFingerPrintSign,
        string? callbackUrl,
        string? returnUrl,
        string? authMessage,
        string? userVisibleData,
        string? userNonVisibleData,
        string? userVisibleDataFormat)
    {
        var request = GetRequestObject<Models.GrandIdRequest>();

        request.Gui = useGui;
        request.Qr = allowQrCode;
        request.AllowFingerprintAuth = allowFingerPrintAuth;
        request.AllowFingerprintSign = allowFingerPrintSign;
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
