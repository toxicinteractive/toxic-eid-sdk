using Models = Toxic.EId.Sdk.Features.BankId.Logout;

namespace Toxic.EId.Sdk.Features.BankId;

public partial class BankIdClient
{
    public virtual async partial Task<Models.LogoutResponse> Logout(string sessionId)
    {
        var request = GetRequestObject<Models.EIdRequest>();
        request.SessionId = sessionId;

        var response = await Call<Models.EIdRequest, Models.EIdResponse>("Logout", request);
        return response.ToEntity();
    }
}
