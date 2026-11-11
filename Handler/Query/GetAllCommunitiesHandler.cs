using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.Handler.Query
{
    public class GetAllCommunitiesHandler : IGetAllCommunitiesHandler
    {
        private readonly ICommunityQueryService _queryService;

        public GetAllCommunitiesHandler(ICommunityQueryService queryService)
        {
            _queryService = queryService;
        }

        public Task<List<CommunityResponse>> HandleAsync(GetAllCommunitiesQuery query, CancellationToken cancellationToken = default)
        {
            return _queryService.GetAllCommunitiesAsync(query, cancellationToken);
        }
    }
}

