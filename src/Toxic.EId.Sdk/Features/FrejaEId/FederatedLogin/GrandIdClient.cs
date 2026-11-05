using Models = Toxic.EId.Sdk.Features.FrejaEId.FederatedLogin;

namespace Toxic.EId.Sdk.Features.FrejaEId;

public partial class FrejaEIdClient : EIdClient
{
    public virtual async partial Task<Models.FederatedLoginResponse> FederatedLogin(
        Func<EIdOptions, string?> serviceKeySelector,
        UserInfoType userInfoType,
        RegistrationLevel minRegistrationLevel,
        bool gui,
        bool qr,
        bool thisDevice,
        bool otherDevice,
        bool deviceChoice,
        bool enableAutoLaunch,
        string? personalNumber,
        string? callbackUrl,
        string? userVisibleData,
        string? userNonVisibleData,
        string? title,
        string? userInfo,
        string? countryCode,
        string? pushNotification,
        string? customerUrl)
    {
        var serviceKey = serviceKeySelector?.Invoke(Options) ?? 
            throw new InvalidOperationException("Invalid Freja eID service key");
            
        var request = GetRequestObject<Models.EIdRequest>(serviceKey);

        request.UserInfoType = userInfoType;
        request.MinRegistrationLevel = minRegistrationLevel;
        request.Gui = gui;
        request.Qr = qr;
        request.ThisDevice = thisDevice;
        request.OtherDevice = otherDevice;
        request.DeviceChoice = deviceChoice;
        request.EnableAutoLaunch = enableAutoLaunch;
        request.PersonalNumber = personalNumber;
        request.CallbackUrl = callbackUrl;
        request.UserVisibleData = userVisibleData;
        request.UserNonVisibleData = userNonVisibleData;
        request.Title = title;
        request.UserInfo = userInfo;
        request.CountryCode = countryCode;
        request.PushNotification = pushNotification;
        request.CustomerUrl = customerUrl;

        var response = await Call<Models.EIdRequest, Models.EIdResponse>("FederatedLogin", request);
        return response.ToEntity();
    }
}
