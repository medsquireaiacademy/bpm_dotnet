using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.Handler.Query
{
    public interface IGetAllMaintenanceRequestsHandler
    {
        Task<PagedResult<MaintenanceTicketDto>> HandleAsync(GetAllMaintenanceRequestsQuery query, CancellationToken cancellationToken = default);
    }

    public class GetAllMaintenanceRequestsHandler : IGetAllMaintenanceRequestsHandler
    {
        private readonly IMaintenanceQueryService _service;
        public GetAllMaintenanceRequestsHandler(IMaintenanceQueryService service) => _service = service;
        public Task<PagedResult<MaintenanceTicketDto>> HandleAsync(GetAllMaintenanceRequestsQuery query, CancellationToken cancellationToken = default) =>
            _service.GetAllRequestsAsync(query, cancellationToken);
    }

    public interface IGetMaintenanceRequestByIdHandler
    {
        Task<MaintenanceTicketDetailDto?> HandleAsync(GetMaintenanceRequestByIdQuery query, CancellationToken cancellationToken = default);
    }

    public class GetMaintenanceRequestByIdHandler : IGetMaintenanceRequestByIdHandler
    {
        private readonly IMaintenanceQueryService _service;
        public GetMaintenanceRequestByIdHandler(IMaintenanceQueryService service) => _service = service;
        public Task<MaintenanceTicketDetailDto?> HandleAsync(GetMaintenanceRequestByIdQuery query, CancellationToken cancellationToken = default) =>
            _service.GetRequestByIdAsync(query, cancellationToken);
    }

    public interface IGetCompatibleAgenciesHandler
    {
        Task<List<CompatibleAgencyDto>> HandleAsync(GetCompatibleAgenciesQuery query, CancellationToken cancellationToken = default);
    }

    public class GetCompatibleAgenciesHandler : IGetCompatibleAgenciesHandler
    {
        private readonly IMaintenanceQueryService _service;
        public GetCompatibleAgenciesHandler(IMaintenanceQueryService service) => _service = service;
        public Task<List<CompatibleAgencyDto>> HandleAsync(GetCompatibleAgenciesQuery query, CancellationToken cancellationToken = default) =>
            _service.GetCompatibleAgenciesAsync(query, cancellationToken);
    }
}
