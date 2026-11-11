using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;

namespace beeproperties.admin.webapi.Handler.Query
{
    public interface IGetAgencyByIdHandler
    {
        Task<AgencyDetailResponse?> HandleAsync(GetAgencyByIdQuery query, CancellationToken cancellationToken = default);
    }
}

