using beeproperties.admin.webapi.repository.Data;
using beeproperties.admin.webapi.repository.Entities;
using beeproperties.admin.webapi.repository.Query.Interface;

namespace beeproperties.admin.webapi.repository.Query
{
    public class CommunityQueryRepository : ICommunityQueryRepository
    {
        private readonly ICommunityDataStore _dataStore;

        public CommunityQueryRepository(ICommunityDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<List<CommunityEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var results = _dataStore.GetAll().ToList();
            return Task.FromResult(results);
        }

        public Task<CommunityEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
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

