using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.Handler.Query
{
    public class GetCommunityByIdHandler : IGetCommunityByIdHandler
    {
        private readonly ICommunityQueryService _queryService;

        public GetCommunityByIdHandler(ICommunityQueryService queryService)
        {
            _queryService = queryService;
        }

        public Task<CommunityDetailResponse?> HandleAsync(GetCommunityByIdQuery query, CancellationToken cancellationToken = default)
        {
            return _queryService.GetCommunityByIdAsync(query, cancellationToken);
        }
    }
}

