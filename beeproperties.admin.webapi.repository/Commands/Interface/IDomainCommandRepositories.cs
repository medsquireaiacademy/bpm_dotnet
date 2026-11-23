using beeproperties.admin.webapi.repository.Entities;

namespace beeproperties.admin.webapi.repository.Commands.Interface
{
    public interface IPropertyCommandRepository
    {
        Task<PropertyEntity> AddPropertyAsync(PropertyEntity property, CancellationToken cancellationToken = default);
        Task<PropertyEntity?> UpdatePropertyAsync(PropertyEntity property, CancellationToken cancellationToken = default);
        Task<bool> DeletePropertyAsync(string id, CancellationToken cancellationToken = default);
    }

    public interface IMaintenanceCommandRepository
    {
        Task<MaintenanceRequestEntity> AddRequestAsync(MaintenanceRequestEntity request, CancellationToken cancellationToken = default);
        Task<MaintenanceRequestEntity?> UpdateRequestAsync(MaintenanceRequestEntity request, CancellationToken cancellationToken = default);
        Task<bool> DeleteRequestAsync(string id, CancellationToken cancellationToken = default);
    }

    public interface ISecurityCommandRepository
    {
        Task<SecurityDeploymentEntity> AddDeploymentAsync(SecurityDeploymentEntity deployment, CancellationToken cancellationToken = default);
        Task<SecurityDeploymentEntity?> UpdateDeploymentAsync(SecurityDeploymentEntity deployment, CancellationToken cancellationToken = default);
        Task<bool> DeleteDeploymentAsync(string id, CancellationToken cancellationToken = default);
    }

    public interface ISettingsCommandRepository
    {
        Task<ServiceCategoryEntity> AddCategoryAsync(ServiceCategoryEntity category, CancellationToken cancellationToken = default);
        Task<ServiceCategoryEntity?> UpdateCategoryAsync(ServiceCategoryEntity category, CancellationToken cancellationToken = default);
        Task<bool> DeleteCategoryAsync(string id, CancellationToken cancellationToken = default);
        Task SetLookupValuesAsync(string group, List<string> values, CancellationToken cancellationToken = default);
        Task<AppPreferencesEntity> UpdatePreferencesAsync(AppPreferencesEntity preferences, CancellationToken cancellationToken = default);
        Task ResetDataAsync(CancellationToken cancellationToken = default);
    }

    public interface IAuthCommandRepository
    {
        Task<AdminUserEntity> UpdateUserAsync(AdminUserEntity user, CancellationToken cancellationToken = default);
        Task AddActivityAsync(ActivityEntity activity, CancellationToken cancellationToken = default);
    }
}
