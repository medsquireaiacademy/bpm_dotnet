using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;

namespace beeproperties.admin.webapi.Handler.Query
{
    public interface IGetCommunityByIdHandler
    {
        Task<CommunityDetailResponse?> HandleAsync(GetCommunityByIdQuery query, CancellationToken cancellationToken = default);
    }
}

