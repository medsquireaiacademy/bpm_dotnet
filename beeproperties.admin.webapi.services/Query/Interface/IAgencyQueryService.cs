using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;

namespace beeproperties.admin.webapi.services.Query.Interface
{
    public interface IAgencyQueryService
    {
        Task<List<AgencyResponse>> GetAllAgenciesAsync(GetAllAgenciesQuery query, CancellationToken cancellationToken = default);
        Task<AgencyDetailResponse?> GetAgencyByIdAsync(GetAgencyByIdQuery query, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);

        Task<List<AgencyPersonnelDto>> GetAgencyPersonnelAsync(GetAgencyPersonnelQuery query, CancellationToken cancellationToken = default);
        Task<List<AgencyPropertyAssignmentDto>> GetAgencyPropertyAssignmentsAsync(GetAgencyPropertyAssignmentsQuery query, CancellationToken cancellationToken = default);
        Task<List<AgencyApprovalHistoryDto>> GetAgencyApprovalHistoryAsync(GetAgencyApprovalHistoryQuery query, CancellationToken cancellationToken = default);
    }
}
