using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Hybrid;
using BankId = Toxic.EId.Sdk.Features.BankId;
using FrejaEId = Toxic.EId.Sdk.Features.FrejaEId;
using Toxic.EId.Sdk.SampleApp.Models;

namespace Toxic.EId.Sdk.SampleApp.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly BankId.BankIdClient _bankIdClient;
    private readonly FrejaEId.FrejaEIdClient _frejaEIdClient;
    private readonly HybridCache _cache;

    public AuthController(
        BankId.BankIdClient bankIdClient,
        FrejaEId.FrejaEIdClient frejaEIdClient,
        HybridCache cache)
    {
        _bankIdClient = bankIdClient;
        _frejaEIdClient = frejaEIdClient;
        _cache = cache;
    }

    [HttpPost("bankid/create")]
    public async Task<ActionResult<BankId.FederatedLogin.FederatedLoginResponse>> CreateBankIdSession(CreateBankIdSessionRequest request)
    {
        var response = await _bankIdClient.FederatedLogin(
            serviceKeySelector: options => request.IsSigning ? options.BankId.SignServiceKey : options.BankId.AuthServiceKey,
            request.Gui,
            request.Qr,
            request.AllowFingerprintAuth,
            request.AllowFingerprintSign,
            request.MobileBankId,
            request.DesktopBankId,
            request.ThisDevice,
            request.CallbackUrl,
            request.ReturnUrl,
            request.AuthMessage,
            request.UserVisibleData,
            request.UserNonVisibleData,
            request.UserVisibleDataFormat);

        // remember the service mode for this session (auth or sign)
        await _cache.SetAsync(response.SessionId!, request.IsSigning);

        return Ok(response);
    }

    [HttpPost("bankid/poll")]
    public async Task<ActionResult<BankId.GetSession.GetSessionResponse>> PollBankIdSession(PollSessionRequest request)
    {
        var isSigning = await _cache.GetOrCreateAsync(request.SessionId!, async _ => false);

        var response = await _bankIdClient.GetSession(
            serviceKeySelector: options => isSigning ? options.BankId.SignServiceKey : options.BankId.AuthServiceKey,
            request.SessionId!);

        return Ok(response);
    }

    [HttpPost("bankid/logout")]
    public async Task<IActionResult> BankIdLogout(LogoutRequest request)
    {
        var isSigning = await _cache.GetOrCreateAsync(request.SessionId!, async _ => false);

        await _bankIdClient.Logout(
            serviceKeySelector: options => isSigning ? options.BankId.SignServiceKey : options.BankId.AuthServiceKey,
            request.SessionId!); 

        return Ok();
    }

    [HttpGet("bankid/callback")]
    public async Task<ActionResult<BankId.GetSession.GetSessionResponse>> BankIdCallback(string grandIdSession)
    {
        var response = await PollBankIdSession(new PollSessionRequest
        {
            SessionId = grandIdSession
        });

        var isSigning = await _cache.GetOrCreateAsync(grandIdSession, async _ => false);

        await _bankIdClient.Logout(
            serviceKeySelector: options => isSigning ? options.BankId.SignServiceKey : options.BankId.AuthServiceKey,
            grandIdSession);

        return response;
    }

    [HttpPost("freja/create")]
    public async Task<ActionResult<FrejaEId.FederatedLogin.FederatedLoginResponse>> CreateFrejaSession(CreateFrejaEIdSessionRequest request)
    {
        var response = await _frejaEIdClient.FederatedLogin(
            serviceKeySelector: options => request.IsSigning ? options.FrejaEId.SignServiceKey : options.FrejaEId.AuthServiceKey,
            Enum.Parse<FrejaEId.UserInfoType>(request.UserInfoType!, ignoreCase: true),
            Enum.Parse<FrejaEId.RegistrationLevel>(request.MinRegistrationLevel!, ignoreCase: true),
            request.Gui,
            request.Qr,
            request.ThisDevice,
            request.OtherDevice,
            request.DeviceChoice,
            request.EnableAutoLaunch,
            request.PersonalNumber,
            request.CallbackUrl,
            request.UserVisibleData,
            request.UserNonVisibleData,
            request.Title,
            request.UserInfo,
            request.CountryCode,
            request.PushNotification,
            request.CustomerUrl);

        // remember the service mode for this session (auth or sign)
        await _cache.SetAsync(response.SessionId!, request.IsSigning);

        return Ok(response);
    }

    [HttpPost("freja/poll")]
    public async Task<ActionResult<FrejaEId.GetSession.GetSessionResponse>> PollFrejaSession(PollSessionRequest request)
    {
        var isSigning = await _cache.GetOrCreateAsync(request.SessionId!, async _ => false);

        var response = await _frejaEIdClient.GetSession(
            serviceKeySelector: options => isSigning ? options.FrejaEId.SignServiceKey : options.FrejaEId.AuthServiceKey,
            request.SessionId!); 

        return Ok(response);
    }

    [HttpPost("freja/logout")]
    public async Task<IActionResult> FrejaLogout(LogoutRequest request)
    {
        var isSigning = await _cache.GetOrCreateAsync(request.SessionId!, async _ => false);

        await _frejaEIdClient.Logout(
            serviceKeySelector: options => isSigning ? options.FrejaEId.SignServiceKey : options.FrejaEId.AuthServiceKey,
            request.SessionId!); 

        return Ok();
    }

    [HttpGet("freja/callback")]
    public async Task<ActionResult<FrejaEId.GetSession.GetSessionResponse>> FrejaCallback(string grandIdSession)
    {
        var response = await PollFrejaSession(new PollSessionRequest
        {
            SessionId = grandIdSession
        });

        var isSigning = await _cache.GetOrCreateAsync(grandIdSession, async _ => false);

        await _frejaEIdClient.Logout(
            serviceKeySelector: options => isSigning ? options.FrejaEId.SignServiceKey : options.FrejaEId.AuthServiceKey,
            grandIdSession); 

        return response;
    }
}
