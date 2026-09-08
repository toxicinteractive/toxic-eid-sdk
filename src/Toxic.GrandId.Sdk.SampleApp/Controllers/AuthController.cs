using Microsoft.AspNetCore.Mvc;
using Toxic.GrandId.Sdk.Features.FederatedLogin;
using Toxic.GrandId.Sdk.Features.GetSession;
using Toxic.GrandId.Sdk.SampleApp.Models;

namespace Toxic.GrandId.Sdk.SampleApp.Controllers;

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
            request.UseGui,
            request.AllowQr,
            request.AllowFingerprintAuth,
            request.AllowFingerprintSign,
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

    // [HttpPost]
    // public async Task<ActionResult<GetSessionResponse>> Callback(string grandidsession)
    // {
        
    // }
}
