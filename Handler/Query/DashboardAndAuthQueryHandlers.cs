using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.Handler.Query
{
    public interface IGetDashboardMetricsHandler
    {
        Task<DashboardMetricsDto> HandleAsync(GetDashboardMetricsQuery query, CancellationToken cancellationToken = default);
    }

    public class GetDashboardMetricsHandler : IGetDashboardMetricsHandler
    {
        private readonly IDashboardQueryService _service;
        public GetDashboardMetricsHandler(IDashboardQueryService service) => _service = service;
        public Task<DashboardMetricsDto> HandleAsync(GetDashboardMetricsQuery query, CancellationToken cancellationToken = default) =>
            _service.GetDashboardMetricsAsync(query, cancellationToken);
    }

    public interface IGetServiceRequestChartsHandler
    {
        Task<ServiceRequestChartsDto> HandleAsync(GetServiceRequestChartsQuery query, CancellationToken cancellationToken = default);
    }

    public class GetServiceRequestChartsHandler : IGetServiceRequestChartsHandler
    {
        private readonly IDashboardQueryService _service;
        public GetServiceRequestChartsHandler(IDashboardQueryService service) => _service = service;
        public Task<ServiceRequestChartsDto> HandleAsync(GetServiceRequestChartsQuery query, CancellationToken cancellationToken = default) =>
            _service.GetServiceRequestChartsAsync(query, cancellationToken);
    }

    public interface IGetPendingApprovalsHandler
    {
        Task<PendingApprovalsSummaryDto> HandleAsync(GetPendingApprovalsQuery query, CancellationToken cancellationToken = default);
    }

    public class GetPendingApprovalsHandler : IGetPendingApprovalsHandler
    {
        private readonly IDashboardQueryService _service;
        public GetPendingApprovalsHandler(IDashboardQueryService service) => _service = service;
        public Task<PendingApprovalsSummaryDto> HandleAsync(GetPendingApprovalsQuery query, CancellationToken cancellationToken = default) =>
            _service.GetPendingApprovalsAsync(query, cancellationToken);
    }

    public interface IGetCurrentUserHandler
    {
        Task<AdminUserDto?> HandleAsync(GetCurrentUserQuery query, CancellationToken cancellationToken = default);
    }

    public class GetCurrentUserHandler : IGetCurrentUserHandler
    {
        private readonly IAuthQueryService _service;
        public GetCurrentUserHandler(IAuthQueryService service) => _service = service;
        public Task<AdminUserDto?> HandleAsync(GetCurrentUserQuery query, CancellationToken cancellationToken = default) =>
            _service.GetCurrentUserAsync(query, cancellationToken);
    }

    public interface IGetRecentActivitiesHandler
    {
        Task<List<ActivityDto>> HandleAsync(GetRecentActivitiesQuery query, CancellationToken cancellationToken = default);
    }

    public class GetRecentActivitiesHandler : IGetRecentActivitiesHandler
    {
        private readonly IAuthQueryService _service;
        public GetRecentActivitiesHandler(IAuthQueryService service) => _service = service;
        public Task<List<ActivityDto>> HandleAsync(GetRecentActivitiesQuery query, CancellationToken cancellationToken = default) =>
            _service.GetRecentActivitiesAsync(query, cancellationToken);
    }
}
