using beeproperties.admin.webapi.repository.Data;
using beeproperties.admin.webapi.repository.Entities;
using beeproperties.admin.webapi.repository.Query.Interface;

namespace beeproperties.admin.webapi.repository.Query
{
    public class PropertyQueryRepository : IPropertyQueryRepository
    {
        private readonly IPropertyDataStore _dataStore;

        public PropertyQueryRepository(IPropertyDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<List<PropertyEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dataStore.GetAll().ToList());
        }

        public Task<PropertyEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dataStore.GetById(id));
        }

        public Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dataStore.Exists(id));
        }
    }

    public class MaintenanceQueryRepository : IMaintenanceQueryRepository
    {
        private readonly IMaintenanceDataStore _dataStore;

        public MaintenanceQueryRepository(IMaintenanceDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<List<MaintenanceRequestEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dataStore.GetAll().ToList());
        }

        public Task<MaintenanceRequestEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dataStore.GetById(id));
        }

        public Task<List<MaintenanceRequestEntity>> GetByPropertyIdAsync(string propertyId, CancellationToken cancellationToken = default)
        {
            var results = _dataStore.GetAll()
                .Where(r => string.Equals(r.PropertyId, propertyId, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Task.FromResult(results);
        }

        public Task<List<MaintenanceRequestEntity>> GetByAgencyIdAsync(string agencyId, CancellationToken cancellationToken = default)
        {
            var results = _dataStore.GetAll()
                .Where(r => string.Equals(r.AssignedAgencyId, agencyId, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Task.FromResult(results);
        }
    }

    public class SecurityQueryRepository : ISecurityQueryRepository
    {
        private readonly ISecurityDataStore _dataStore;

        public SecurityQueryRepository(ISecurityDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<List<SecurityDeploymentEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dataStore.GetAll().ToList());
        }

        public Task<SecurityDeploymentEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dataStore.GetById(id));
        }

        public Task<List<SecurityDeploymentEntity>> GetByPropertyIdAsync(string propertyId, CancellationToken cancellationToken = default)
        {
            var results = _dataStore.GetAll()
                .Where(d => string.Equals(d.PropertyId, propertyId, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Task.FromResult(results);
        }
    }

    public class SettingsQueryRepository : ISettingsQueryRepository
    {
        private readonly ISettingsDataStore _dataStore;

        public SettingsQueryRepository(ISettingsDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<List<ServiceCategoryEntity>> GetServiceCategoriesAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dataStore.GetServiceCategories().ToList());
        }

        public Task<ServiceCategoryEntity?> GetServiceCategoryByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dataStore.GetServiceCategoryById(id));
        }

        public Task<List<string>> GetLookupsAsync(string group, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dataStore.GetLookupValues(group));
        }

        public Task<AppPreferencesEntity> GetPreferencesAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dataStore.GetPreferences());
        }
    }

    public class AuthQueryRepository : IAuthQueryRepository
    {
        private readonly IAuthDataStore _dataStore;

        public AuthQueryRepository(IAuthDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<AdminUserEntity?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dataStore.GetUserByUsername(username));
        }

        public Task<AdminUserEntity?> GetUserByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dataStore.GetUserById(id));
        }

        public Task<List<ActivityEntity>> GetRecentActivitiesAsync(int limit = 10, string? type = null, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dataStore.GetRecentActivities(limit, type).ToList());
        }
    }
}
