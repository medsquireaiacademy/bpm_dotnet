using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;

namespace beeproperties.admin.webapi.services.Query.Interface
{
    public interface ICommunityQueryService
    {
        Task<List<CommunityResponse>> GetAllCommunitiesAsync(GetAllCommunitiesQuery query, CancellationToken cancellationToken = default);
        Task<CommunityDetailResponse?> GetCommunityByIdAsync(GetCommunityByIdQuery query, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    }
}

