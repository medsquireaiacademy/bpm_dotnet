using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;

namespace beeproperties.admin.webapi.Handler.Query
{
    public interface IGetAllCommunitiesHandler
    {
        Task<List<CommunityResponse>> HandleAsync(GetAllCommunitiesQuery query, CancellationToken cancellationToken = default);
    }
}

