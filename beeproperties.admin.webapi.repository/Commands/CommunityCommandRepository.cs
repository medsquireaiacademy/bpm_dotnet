using beeproperties.admin.webapi.repository.Commands.Interface;
using beeproperties.admin.webapi.repository.Data;
using beeproperties.admin.webapi.repository.Entities;

namespace beeproperties.admin.webapi.repository.Commands
{
    public class CommunityCommandRepository : ICommunityCommandRepository
    {
        private readonly ICommunityDataStore _dataStore;

        public CommunityCommandRepository(ICommunityDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<CommunityEntity> AddAsync(CommunityEntity entity, CancellationToken cancellationToken = default)
        {
            var result = _dataStore.Add(entity);
            return Task.FromResult(result);
        }

        public Task<CommunityEntity?> UpdateAsync(CommunityEntity entity, CancellationToken cancellationToken = default)
        {
            var result = _dataStore.Update(entity);
            return Task.FromResult(result);
        }

        public Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var result = _dataStore.Delete(id);
            return Task.FromResult(result);
        }
    }
}

