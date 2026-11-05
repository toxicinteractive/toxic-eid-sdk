using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Toxic.EId.Sdk.Features.FrejaEId.FederatedLogin;
using Toxic.EId.Sdk.Features.FrejaEId.GetSession;
using Toxic.EId.Sdk.Features.FrejaEId.Logout;

namespace Toxic.EId.Sdk.Features.FrejaEId;

/// <summary>
/// A client to create and manage authentication and signing sessions with Freja eID via the eID API.
/// </summary>
public partial class FrejaEIdClient : EIdClient
{
    public FrejaEIdClient(HttpClient httpClient, IOptions<EIdOptions> options, ILogger<FrejaEIdClient> logger) 
        : base(httpClient, options.Value, logger)
    {
        
    }

    /// <summary>
    /// Creates a Freja eID authentication order.
    /// Refer to the eID documentation for Freja eID at https://docs.grandid.com/freja for more information about the parameters.
    /// </summary>
    /// <param name="serviceKeySelector">Provide an auth service key or sign service key depending on the mode chosen.</param>
    /// <param name="userInfoType">The user info type that should be used when authenticating.</param>
    /// <param name="minRegistrationLevel">Minimum registration level for users.</param>
    /// <param name="gui">Set to true to use the EId redirection method.</param>
    /// <param name="qr">Set to true to include QR codes in the response.</param>
    /// <param name="thisDevice">Set to true to allow the Freja eID app to be launched on the current device.</param>
    /// <param name="otherDevice">Set to true to allow the user to manually launch the Freja eID app.</param>
    /// <param name="deviceChoice">Set to true to allow the user to choose which device to launch the Freja eID app on.</param>
    /// <param name="enableAutoLaunch">Set to true to automatically launch the Freja eID app on the current device.</param>
    /// <param name="personalNumber">Used as input for the corresponding user info type.</param>
    /// <param name="callbackUrl">Used to receive a completed session id when the user is authenticating via the eID redirection method (gui).</param>
    /// <param name="userVisibleData">Base64 encoded text to be shown to the user when signing.</param>
    /// <param name="userNonVisibleData">Base64 encoded text that will be included in the signing process but invisible to the user.</param>
    /// <param name="title">Base64 encoded title to show when signing.</param>
    /// <param name="userInfo">Input data corresponding to the user info type.</param>
    /// <param name="countryCode">2-letter country code for when a personal number is provided and is not issued in Sweden.</param>
    /// <param name="pushNotification">Base64 encoded text to display in push notification to the user.</param>
    /// <param name="customerUrl">Used as a link in the GUI mode to link back to the web page.</param>
    /// <returns></returns>
    public virtual partial Task<FederatedLoginResponse> FederatedLogin(
        Func<EIdOptions, string?> serviceKeySelector,
        UserInfoType userInfoType,
        RegistrationLevel minRegistrationLevel,
        bool gui,
        bool qr = false,
        bool thisDevice = false,
        bool otherDevice = false,
        bool deviceChoice = false,
        bool enableAutoLaunch = false,
        string? personalNumber = null,
        string? callbackUrl = null,
        string? userVisibleData = null,
        string? userNonVisibleData = null,
        string? title = null,
        string? userInfo = null,
        string? countryCode = null,
        string? pushNotification = null,
        string? customerUrl = null);

    /// <summary>
    /// Gets information about an ongoing authentication or signing process.
    /// </summary>
    /// <param name="sessionId">A EId session ID.</param>
    /// <param name="serviceKeySelector">Provide an auth service key or sign service key depending on the mode chosen.</param>
    public virtual partial Task<GetSessionResponse> GetSession(
        string sessionId,
        Func<EIdOptions, string?> serviceKeySelector);

    /// <summary>
    /// Cancels and/or deletes an eID session on the remote side.
    /// </summary>
    /// <param name="sessionId">An eID session ID.</param>
    /// <param name="serviceKeySelector">Provide an auth service key or sign service key depending on the mode chosen.</param>
    public virtual partial Task<LogoutResponse> Logout(
        string sessionId,
        Func<EIdOptions, string?> serviceKeySelector);
}

public enum UserInfoType
{
    Inferred,
    Email,
    Phone,
    Ssn,
    Upi
}

public enum RegistrationLevel
{
    Basic,
    Extended,
    Plus
}
