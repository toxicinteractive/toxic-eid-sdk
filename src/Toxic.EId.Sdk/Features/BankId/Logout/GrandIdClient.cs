using Models = Toxic.EId.Sdk.Features.BankId.Logout;

namespace Toxic.EId.Sdk.Features.BankId;

public partial class BankIdClient
{
    public virtual async partial Task<Models.LogoutResponse> Logout(
        string sessionId, 
        Func<EIdOptions, string?> serviceKeySelector)
    {
        var serviceKey = serviceKeySelector?.Invoke(Options) ?? 
            throw new InvalidOperationException("Invalid BankId service key");

        var request = GetRequestObject<Models.EIdRequest>(serviceKey);
        request.SessionId = sessionId;

        var response = await Call<Models.EIdRequest, Models.EIdResponse>("Logout", request);
        return response.ToEntity();
    }
}
