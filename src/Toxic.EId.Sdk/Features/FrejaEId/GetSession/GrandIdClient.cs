using Models = Toxic.EId.Sdk.Features.FrejaEId.GetSession;

namespace Toxic.EId.Sdk.Features.FrejaEId;

public partial class FrejaEIdClient
{
    public virtual async partial Task<Models.GetSessionResponse> GetSession(
        Func<EIdOptions, string?> serviceKeySelector,
        string sessionId)
    {
        var serviceKey = serviceKeySelector?.Invoke(Options) ?? 
            throw new InvalidOperationException("Invalid Freja eID service key");

        var request = GetRequestObject<Models.EIdRequest>(serviceKey);
        request.SessionId = sessionId;

        var response = await Call<Models.EIdRequest, Models.EIdResponse>("GetSession", request);
        return response.ToEntity();
    }
}
