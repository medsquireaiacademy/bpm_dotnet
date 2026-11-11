using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.Handler.Query
{
    public class GetAgencyByIdHandler : IGetAgencyByIdHandler
    {
        private readonly IAgencyQueryService _queryService;

        public GetAgencyByIdHandler(IAgencyQueryService queryService)
        {
            _queryService = queryService;
        }

        public Task<AgencyDetailResponse?> HandleAsync(GetAgencyByIdQuery query, CancellationToken cancellationToken = default)
        {
            return _queryService.GetAgencyByIdAsync(query, cancellationToken);
        }
    }
}

