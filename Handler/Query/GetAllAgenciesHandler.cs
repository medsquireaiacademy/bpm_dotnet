using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.Handler.Query
{
    public class GetAllAgenciesHandler : IGetAllAgenciesHandler
    {
        private readonly IAgencyQueryService _queryService;

        public GetAllAgenciesHandler(IAgencyQueryService queryService)
        {
            _queryService = queryService;
        }

        public Task<List<AgencyResponse>> HandleAsync(GetAllAgenciesQuery query, CancellationToken cancellationToken = default)
        {
            return _queryService.GetAllAgenciesAsync(query, cancellationToken);
        }
    }
}

