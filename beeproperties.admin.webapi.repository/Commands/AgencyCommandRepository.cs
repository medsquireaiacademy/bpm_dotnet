using beeproperties.admin.webapi.repository.Commands.Interface;
using beeproperties.admin.webapi.repository.Data;
using beeproperties.admin.webapi.repository.Entities;

namespace beeproperties.admin.webapi.repository.Commands
{
    public class AgencyCommandRepository : IAgencyCommandRepository
    {
        private readonly IAgencyDataStore _dataStore;

        public AgencyCommandRepository(IAgencyDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<AgencyEntity> AddAsync(AgencyEntity entity, CancellationToken cancellationToken = default)
        {
            var result = _dataStore.Add(entity);
            return Task.FromResult(result);
        }

        public Task<AgencyEntity?> UpdateAsync(AgencyEntity entity, CancellationToken cancellationToken = default)
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

