using beeproperties.admin.webapi.repository.Data;
using beeproperties.admin.webapi.repository.Entities;
using beeproperties.admin.webapi.repository.Commands.Interface;

namespace beeproperties.admin.webapi.repository.Commands
{
    public class PropertyCommandRepository : IPropertyCommandRepository
    {
        private readonly IPropertyDataStore _dataStore;

        public PropertyCommandRepository(IPropertyDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<PropertyEntity> AddPropertyAsync(PropertyEntity property, CancellationToken cancellationToken = default)
        {
            var created = _dataStore.Add(property);
            return Task.FromResult(created);
        }

        public Task<PropertyEntity?> UpdatePropertyAsync(PropertyEntity property, CancellationToken cancellationToken = default)
        {
            var updated = _dataStore.Update(property);
            return Task.FromResult(updated);
        }

        public Task<bool> DeletePropertyAsync(string id, CancellationToken cancellationToken = default)
        {
            var deleted = _dataStore.Delete(id);
            return Task.FromResult(deleted);
        }
    }

    public class MaintenanceCommandRepository : IMaintenanceCommandRepository
    {
        private readonly IMaintenanceDataStore _dataStore;

        public MaintenanceCommandRepository(IMaintenanceDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<MaintenanceRequestEntity> AddRequestAsync(MaintenanceRequestEntity request, CancellationToken cancellationToken = default)
        {
            var created = _dataStore.Add(request);
            return Task.FromResult(created);
        }

        public Task<MaintenanceRequestEntity?> UpdateRequestAsync(MaintenanceRequestEntity request, CancellationToken cancellationToken = default)
        {
            var updated = _dataStore.Update(request);
            return Task.FromResult(updated);
        }

        public Task<bool> DeleteRequestAsync(string id, CancellationToken cancellationToken = default)
        {
            var deleted = _dataStore.Delete(id);
            return Task.FromResult(deleted);
        }
    }

    public class SecurityCommandRepository : ISecurityCommandRepository
    {
        private readonly ISecurityDataStore _dataStore;

        public SecurityCommandRepository(ISecurityDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<SecurityDeploymentEntity> AddDeploymentAsync(SecurityDeploymentEntity deployment, CancellationToken cancellationToken = default)
        {
            var created = _dataStore.Add(deployment);
            return Task.FromResult(created);
        }

        public Task<SecurityDeploymentEntity?> UpdateDeploymentAsync(SecurityDeploymentEntity deployment, CancellationToken cancellationToken = default)
        {
            var updated = _dataStore.Update(deployment);
            return Task.FromResult(updated);
        }

        public Task<bool> DeleteDeploymentAsync(string id, CancellationToken cancellationToken = default)
        {
            var deleted = _dataStore.Delete(id);
            return Task.FromResult(deleted);
        }
    }

    public class SettingsCommandRepository : ISettingsCommandRepository
    {
        private readonly ISettingsDataStore _dataStore;

        public SettingsCommandRepository(ISettingsDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<ServiceCategoryEntity> AddCategoryAsync(ServiceCategoryEntity category, CancellationToken cancellationToken = default)
        {
            var created = _dataStore.AddServiceCategory(category);
            return Task.FromResult(created);
        }

        public Task<ServiceCategoryEntity?> UpdateCategoryAsync(ServiceCategoryEntity category, CancellationToken cancellationToken = default)
        {
            var updated = _dataStore.UpdateServiceCategory(category);
            return Task.FromResult(updated);
        }

        public Task<bool> DeleteCategoryAsync(string id, CancellationToken cancellationToken = default)
        {
            var deleted = _dataStore.DeleteServiceCategory(id);
            return Task.FromResult(deleted);
        }

        public Task SetLookupValuesAsync(string group, List<string> values, CancellationToken cancellationToken = default)
        {
            _dataStore.SetLookupValues(group, values);
            return Task.CompletedTask;
        }

        public Task<AppPreferencesEntity> UpdatePreferencesAsync(AppPreferencesEntity preferences, CancellationToken cancellationToken = default)
        {
            var updated = _dataStore.UpdatePreferences(preferences);
            return Task.FromResult(updated);
        }

        public Task ResetDataAsync(CancellationToken cancellationToken = default)
        {
            _dataStore.ResetAll();
            return Task.CompletedTask;
        }
    }

    public class AuthCommandRepository : IAuthCommandRepository
    {
        private readonly IAuthDataStore _dataStore;

        public AuthCommandRepository(IAuthDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<AdminUserEntity> UpdateUserAsync(AdminUserEntity user, CancellationToken cancellationToken = default)
        {
            var updated = _dataStore.UpdateUser(user);
            return Task.FromResult(updated);
        }

        public Task AddActivityAsync(ActivityEntity activity, CancellationToken cancellationToken = default)
        {
            _dataStore.AddActivity(activity);
            return Task.CompletedTask;
        }
    }
}
