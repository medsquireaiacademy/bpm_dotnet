using beeproperties.admin.webapi.repository.Entities;

namespace beeproperties.admin.webapi.repository.Query.Interface
{
    public interface IPropertyQueryRepository
    {
        Task<List<PropertyEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PropertyEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    }

    public interface IMaintenanceQueryRepository
    {
        Task<List<MaintenanceRequestEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<MaintenanceRequestEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<List<MaintenanceRequestEntity>> GetByPropertyIdAsync(string propertyId, CancellationToken cancellationToken = default);
        Task<List<MaintenanceRequestEntity>> GetByAgencyIdAsync(string agencyId, CancellationToken cancellationToken = default);
    }

    public interface ISecurityQueryRepository
    {
        Task<List<SecurityDeploymentEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<SecurityDeploymentEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<List<SecurityDeploymentEntity>> GetByPropertyIdAsync(string propertyId, CancellationToken cancellationToken = default);
    }

    public interface ISettingsQueryRepository
    {
        Task<List<ServiceCategoryEntity>> GetServiceCategoriesAsync(CancellationToken cancellationToken = default);
        Task<ServiceCategoryEntity?> GetServiceCategoryByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<List<string>> GetLookupsAsync(string group, CancellationToken cancellationToken = default);
        Task<AppPreferencesEntity> GetPreferencesAsync(CancellationToken cancellationToken = default);
    }

    public interface IAuthQueryRepository
    {
        Task<AdminUserEntity?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<AdminUserEntity?> GetUserByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<List<ActivityEntity>> GetRecentActivitiesAsync(int limit = 10, string? type = null, CancellationToken cancellationToken = default);
    }
}
