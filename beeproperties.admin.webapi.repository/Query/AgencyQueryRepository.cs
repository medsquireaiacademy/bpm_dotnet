using beeproperties.admin.webapi.repository.Data;
using beeproperties.admin.webapi.repository.Entities;
using beeproperties.admin.webapi.repository.Query.Interface;

namespace beeproperties.admin.webapi.repository.Query
{
    public class AgencyQueryRepository : IAgencyQueryRepository
    {
        private readonly IAgencyDataStore _dataStore;

        public AgencyQueryRepository(IAgencyDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<List<AgencyEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var results = _dataStore.GetAll().ToList();
            return Task.FromResult(results);
        }

        public Task<AgencyEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var result = _dataStore.GetById(id);
            return Task.FromResult(result);
        }

        public Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
        {
            var exists = _dataStore.Exists(id);
            return Task.FromResult(exists);
        }
    }
}

