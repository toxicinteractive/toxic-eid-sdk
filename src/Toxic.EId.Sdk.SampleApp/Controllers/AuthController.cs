using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Hybrid;
using Toxic.EId.Sdk.Features.BankId;
using Toxic.EId.Sdk.Features.BankId.FederatedLogin;
using Toxic.EId.Sdk.Features.BankId.GetSession;
using Toxic.EId.Sdk.SampleApp.Models;

namespace Toxic.EId.Sdk.SampleApp.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly BankIdClient _bankIdClient;
    private readonly HybridCache _cache;

    public AuthController(BankIdClient bankIdClient, HybridCache cache)
    {
        _bankIdClient = bankIdClient;
        _cache = cache;
    }

    [HttpPost("create")]
    public async Task<ActionResult<FederatedLoginResponse>> CreateSession(CreateSessionRequest request)
    {
        var response = await _bankIdClient.FederatedLogin(
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
            request.UserVisibleDataFormat,
            serviceKeySelector: options => request.IsSigning ? 
                options.BankId.SignServiceKey : 
                options.BankId.AuthServiceKey);

        // remember the service mode for this session (auth or sign)
        await _cache.SetAsync(response.SessionId!, request.IsSigning);

        return Ok(response);
    }

    [HttpPost("poll")]
    public async Task<ActionResult<GetSessionResponse>> PollSession(PollSessionRequest request)
    {
        var isSigning = await _cache.GetOrCreateAsync(request.SessionId!, async _ => false);

        var response = await _bankIdClient.GetSession(
            request.SessionId!, 
            serviceKeySelector: options => isSigning ? 
                options.BankId.SignServiceKey : 
                options.BankId.AuthServiceKey);

        return Ok(response);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutRequest request)
    {
        var isSigning = await _cache.GetOrCreateAsync(request.SessionId!, async _ => false);

        await _bankIdClient.Logout(
            request.SessionId!, 
            serviceKeySelector: options => isSigning ? 
                options.BankId.SignServiceKey : 
                options.BankId.AuthServiceKey);

        return Ok();
    }

    [HttpGet("callback")]
    public async Task<ActionResult<GetSessionResponse>> Callback(string grandIdSession)
    {
        var response = await PollSession(new PollSessionRequest
        {
            SessionId = grandIdSession
        });

        var isSigning = await _cache.GetOrCreateAsync(grandIdSession, async _ => false);

        await _bankIdClient.Logout(
            grandIdSession, 
            serviceKeySelector: options => isSigning ? 
                options.BankId.SignServiceKey : 
                options.BankId.AuthServiceKey);

        return response;
    }
}
