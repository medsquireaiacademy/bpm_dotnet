using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;

namespace beeproperties.admin.webapi.services.Commands.Interface
{
    public interface IAgencyCommandService
    {
        Task<AgencyDetailResponse> CreateAgencyAsync(CreateAgencyCommand command, CancellationToken cancellationToken = default);
        Task<AgencyDetailResponse?> UpdateAgencyAsync(UpdateAgencyCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteAgencyAsync(DeleteAgencyCommand command, CancellationToken cancellationToken = default);

        Task<AgencyDetailResponse?> ReviewRegistrationAsync(ReviewAgencyRegistrationCommand command, CancellationToken cancellationToken = default);
        Task<AgencyPersonnelDto?> AddPersonnelAsync(CreateAgencyPersonnelCommand command, CancellationToken cancellationToken = default);
        Task<AgencyPersonnelDto?> UpdatePersonnelAsync(UpdateAgencyPersonnelCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeletePersonnelAsync(DeleteAgencyPersonnelCommand command, CancellationToken cancellationToken = default);

        Task<AgencyPropertyAssignmentDto?> RequestPropertyAssignmentAsync(RequestPropertyAssignmentCommand command, CancellationToken cancellationToken = default);
        Task<AgencyPropertyAssignmentDto?> ReviewPropertyAssignmentAsync(ReviewPropertyAssignmentCommand command, CancellationToken cancellationToken = default);
        Task<bool> RevokePropertyAssignmentAsync(RevokePropertyAssignmentCommand command, CancellationToken cancellationToken = default);

        Task<AgencyServiceDto?> AddServiceAsync(CreateAgencyServiceCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteServiceAsync(DeleteAgencyServiceCommand command, CancellationToken cancellationToken = default);

        Task<AgencyDocumentDto?> AddDocumentAsync(AddAgencyDocumentCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteDocumentAsync(DeleteAgencyDocumentCommand command, CancellationToken cancellationToken = default);
    }
}
