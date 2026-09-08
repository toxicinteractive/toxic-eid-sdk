using Models = Toxic.GrandId.Sdk.Features.GetSession;

namespace Toxic.GrandId.Sdk;

public abstract partial class GrandIdClient
{
    public virtual async partial Task<Models.GetSessionResponse> GetSession(string sessionId)
    {
        var request = GetRequestObject<Models.GrandIdRequest>();
        request.SessionId = sessionId;

        var response = await Call<Models.GrandIdRequest, Models.GrandIdResponse>("GetSession", request);
        return response.ToEntity();
    }
}
