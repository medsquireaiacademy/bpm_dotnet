using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;

namespace beeproperties.admin.webapi.services.Query.Interface
{
    public interface IPropertyQueryService
    {
        Task<List<PropertyResponse>> GetAllPropertiesAsync(GetAllPropertiesQuery query, CancellationToken cancellationToken = default);
        Task<PropertyDetailResponse?> GetPropertyByIdAsync(GetPropertyByIdQuery query, CancellationToken cancellationToken = default);
        Task<PropertySummaryDto> GetPropertySummaryAsync(GetPropertySummaryQuery query, CancellationToken cancellationToken = default);
        Task<List<PropertyBlockDetailDto>> GetPropertyBlocksAsync(GetPropertyBlocksQuery query, CancellationToken cancellationToken = default);
        Task<List<FloorDto>> GetBlockFloorsAsync(GetBlockFloorsQuery query, CancellationToken cancellationToken = default);
        Task<List<FlatDto>> GetFloorFlatsAsync(GetFloorFlatsQuery query, CancellationToken cancellationToken = default);
        Task<FlatDto?> GetFlatByNumberAsync(GetFlatByNumberQuery query, CancellationToken cancellationToken = default);
        Task<List<PropertyDocumentDto>> GetPropertyDocumentsAsync(GetPropertyDocumentsQuery query, CancellationToken cancellationToken = default);
        Task<RwaDetailResponseDto?> GetPropertyRwaAsync(GetPropertyRwaQuery query, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    }

    public interface IMaintenanceQueryService
    {
        Task<PagedResult<MaintenanceTicketDto>> GetAllRequestsAsync(GetAllMaintenanceRequestsQuery query, CancellationToken cancellationToken = default);
        Task<MaintenanceTicketDetailDto?> GetRequestByIdAsync(GetMaintenanceRequestByIdQuery query, CancellationToken cancellationToken = default);
        Task<List<CompatibleAgencyDto>> GetCompatibleAgenciesAsync(GetCompatibleAgenciesQuery query, CancellationToken cancellationToken = default);
    }

    public interface ISecurityQueryService
    {
        Task<List<SecurityDeploymentDto>> GetAllDeploymentsAsync(GetAllSecurityDeploymentsQuery query, CancellationToken cancellationToken = default);
        Task<SecurityDeploymentDto?> GetDeploymentByIdAsync(GetSecurityDeploymentByIdQuery query, CancellationToken cancellationToken = default);
    }

    public interface ISettingsQueryService
    {
        Task<List<ServiceCategoryDto>> GetAllServiceCategoriesAsync(GetAllServiceCategoriesQuery query, CancellationToken cancellationToken = default);
        Task<MasterLookupsDto> GetMasterLookupsAsync(GetMasterLookupsQuery query, CancellationToken cancellationToken = default);
        Task<AppPreferencesDto> GetAppPreferencesAsync(GetAppPreferencesQuery query, CancellationToken cancellationToken = default);
    }

    public interface IAuthQueryService
    {
        Task<AdminUserDto?> GetCurrentUserAsync(GetCurrentUserQuery query, CancellationToken cancellationToken = default);
        Task<List<ActivityDto>> GetRecentActivitiesAsync(GetRecentActivitiesQuery query, CancellationToken cancellationToken = default);
    }

    public interface IDashboardQueryService
    {
        Task<DashboardMetricsDto> GetDashboardMetricsAsync(GetDashboardMetricsQuery query, CancellationToken cancellationToken = default);
        Task<ServiceRequestChartsDto> GetServiceRequestChartsAsync(GetServiceRequestChartsQuery query, CancellationToken cancellationToken = default);
        Task<PendingApprovalsSummaryDto> GetPendingApprovalsAsync(GetPendingApprovalsQuery query, CancellationToken cancellationToken = default);
    }
}
