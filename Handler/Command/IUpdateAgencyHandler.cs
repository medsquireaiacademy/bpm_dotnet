using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;

namespace beeproperties.admin.webapi.Handler.Command
{
    public interface IUpdateAgencyHandler
    {
        Task<AgencyDetailResponse?> HandleAsync(UpdateAgencyCommand command, CancellationToken cancellationToken = default);
    }
}

