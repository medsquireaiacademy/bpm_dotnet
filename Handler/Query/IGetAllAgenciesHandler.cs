using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;

namespace beeproperties.admin.webapi.Handler.Query
{
    public interface IGetAllAgenciesHandler
    {
        Task<List<AgencyResponse>> HandleAsync(GetAllAgenciesQuery query, CancellationToken cancellationToken = default);
    }
}

