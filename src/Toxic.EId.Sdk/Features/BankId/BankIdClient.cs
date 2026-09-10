using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Toxic.EId.Sdk.Features.BankId.FederatedLogin;
using Toxic.EId.Sdk.Features.BankId.GetSession;
using Toxic.EId.Sdk.Features.BankId.Logout;

namespace Toxic.EId.Sdk.Features.BankId;

/// <summary>
/// A client to create and manage authentication and signing sessions with BankID via the eID API.
/// </summary>
public partial class BankIdClient : EIdClient
{
    public BankIdClient(HttpClient httpClient, IOptions<EIdOptions> options, ILogger<BankIdClient> logger) 
        : base(httpClient, options.Value, logger)
    {
        
    }

    /// <summary>
    /// Creates a BankID authentication order.
    /// </summary>
    /// <param name="gui">Set to true to use the EId redirection method.</param>
    /// <param name="qr">Set to true to include QR codes in the response.</param>
    /// <param name="allowFingerPrintAuth">Set to true to allow fingerprint for authentication.</param>
    /// <param name="allowFingerPrintSign">Set to true to allow fingerprint for signing.</param>
    /// <param name="mobileBankId">Set to true to force the use of mobile BankID certificates when using custom integrations (not gui).</param>
    /// <param name="desktopBankId">Set to true to force the use of desktop BankID certificates when using custom integrations (not gui).</param>
    /// <param name="thisDevice">Set to true to allow the user to authenticate or sign using their current device.</param>
    /// <param name="callbackUrl">Used to receive a completed session id when the user is authenticating via the EId redirection method (gui).</param>
    /// <param name="returnUrl">Used to redirect the user from the BankID app after successful authentication.</param>
    /// <param name="authMessage">Base64 encoded message to be shown to the user when authenticating.</param>
    /// <param name="userVisibleData">Base64 encoded text to be shown to the user when signing.</param>
    /// <param name="userNonVisibleData">Base64 encoded text that will be included in the signing process but invisible to the user.</param>
    /// <param name="userVisibleDataFormat">Can be plaintext or simpleMarkdownV1.</param>
    public virtual partial Task<FederatedLoginResponse> FederatedLogin(
        bool gui,
        bool qr,
        bool allowFingerPrintAuth,
        bool allowFingerPrintSign,
        bool mobileBankId,
        bool desktopBankId,
        bool thisDevice,
        string? callbackUrl = null,
        string? returnUrl = null,
        string? authMessage = null,
        string? userVisibleData = null,
        string? userNonVisibleData = null,
        string? userVisibleDataFormat = null);

    /// <summary>
    /// Gets information about an ongoing authentication or signing process.
    /// </summary>
    /// <param name="sessionId">A EId session ID.</param>
    public virtual partial Task<GetSessionResponse> GetSession(string sessionId);

    /// <summary>
    /// Cancels and/or deletes a EId session on the remote side.
    /// </summary>
    /// <param name="sessionId">A EId session ID.</param>
    public virtual partial Task<LogoutResponse> Logout(string sessionId);
}
