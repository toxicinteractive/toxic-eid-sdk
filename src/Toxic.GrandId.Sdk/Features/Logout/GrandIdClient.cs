using Models = Toxic.GrandId.Sdk.Features.Logout;

namespace Toxic.GrandId.Sdk;

public abstract partial class GrandIdClient
{
    public virtual async partial Task<Models.LogoutResponse> Logout(string sessionId)
    {
        var request = GetRequestObject<Features.Logout.GrandIdRequest>();
        request.SessionId = sessionId;

        var response = await Call<Models.GrandIdRequest, Models.GrandIdResponse>("Logout", request);
        return response.ToEntity();
    }
}
