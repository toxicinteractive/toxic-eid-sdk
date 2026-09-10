using Models = Toxic.EId.Sdk.Features.BankId.GetSession;

namespace Toxic.EId.Sdk.Features.BankId;

public partial class BankIdClient
{
    public virtual async partial Task<Models.GetSessionResponse> GetSession(string sessionId)
    {
        var request = GetRequestObject<Models.EIdRequest>();
        request.SessionId = sessionId;

        var response = await Call<Models.EIdRequest, Models.EIdResponse>("GetSession", request);
        return response.ToEntity();
    }
}
