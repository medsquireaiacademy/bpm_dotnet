using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;

namespace beeproperties.admin.webapi.services.Commands.Interface
{
    public interface IAgencyCommandService
    {
        Task<AgencyDetailResponse> CreateAgencyAsync(CreateAgencyCommand command, CancellationToken cancellationToken = default);
        Task<AgencyDetailResponse?> UpdateAgencyAsync(UpdateAgencyCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteAgencyAsync(DeleteAgencyCommand command, CancellationToken cancellationToken = default);
    }
}

