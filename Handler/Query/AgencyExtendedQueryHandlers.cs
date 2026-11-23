using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.Handler.Query
{
    public interface IGetAgencyPersonnelHandler
    {
        Task<List<AgencyPersonnelDto>> HandleAsync(GetAgencyPersonnelQuery query, CancellationToken cancellationToken = default);
    }

    public class GetAgencyPersonnelHandler : IGetAgencyPersonnelHandler
    {
        private readonly IAgencyQueryService _service;
        public GetAgencyPersonnelHandler(IAgencyQueryService service) => _service = service;
        public Task<List<AgencyPersonnelDto>> HandleAsync(GetAgencyPersonnelQuery query, CancellationToken cancellationToken = default) =>
            _service.GetAgencyPersonnelAsync(query, cancellationToken);
    }

    public interface IGetAgencyPropertyAssignmentsHandler
    {
        Task<List<AgencyPropertyAssignmentDto>> HandleAsync(GetAgencyPropertyAssignmentsQuery query, CancellationToken cancellationToken = default);
    }

    public class GetAgencyPropertyAssignmentsHandler : IGetAgencyPropertyAssignmentsHandler
    {
        private readonly IAgencyQueryService _service;
        public GetAgencyPropertyAssignmentsHandler(IAgencyQueryService service) => _service = service;
        public Task<List<AgencyPropertyAssignmentDto>> HandleAsync(GetAgencyPropertyAssignmentsQuery query, CancellationToken cancellationToken = default) =>
            _service.GetAgencyPropertyAssignmentsAsync(query, cancellationToken);
    }

    public interface IGetAgencyApprovalHistoryHandler
    {
        Task<List<AgencyApprovalHistoryDto>> HandleAsync(GetAgencyApprovalHistoryQuery query, CancellationToken cancellationToken = default);
    }

    public class GetAgencyApprovalHistoryHandler : IGetAgencyApprovalHistoryHandler
    {
        private readonly IAgencyQueryService _service;
        public GetAgencyApprovalHistoryHandler(IAgencyQueryService service) => _service = service;
        public Task<List<AgencyApprovalHistoryDto>> HandleAsync(GetAgencyApprovalHistoryQuery query, CancellationToken cancellationToken = default) =>
            _service.GetAgencyApprovalHistoryAsync(query, cancellationToken);
    }
}
