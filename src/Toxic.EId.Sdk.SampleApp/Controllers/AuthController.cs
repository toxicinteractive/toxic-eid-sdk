using Microsoft.AspNetCore.Mvc;
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

    public AuthController(BankIdClient bankIdClient)
    {
        _bankIdClient = bankIdClient;
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
            request.UserVisibleDataFormat);

        return Ok(response);
    }

    [HttpPost("poll")]
    public async Task<ActionResult<GetSessionResponse>> PollSession(PollSessionRequest request)
    {
        var response = await _bankIdClient.GetSession(request.SessionId!);
        return Ok(response);
    }

    [HttpGet("callback")]
    public async Task<ActionResult<GetSessionResponse>> Callback(string grandIdSession)
    {
        var response = await PollSession(new PollSessionRequest
        {
            SessionId = grandIdSession
        });

        await _bankIdClient.Logout(grandIdSession);

        return response;
    }
}
