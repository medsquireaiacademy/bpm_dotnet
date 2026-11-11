using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;

namespace beeproperties.admin.webapi.Handler.Command
{
    public interface ICreateAgencyHandler
    {
        Task<AgencyDetailResponse> HandleAsync(CreateAgencyCommand command, CancellationToken cancellationToken = default);
    }
}

