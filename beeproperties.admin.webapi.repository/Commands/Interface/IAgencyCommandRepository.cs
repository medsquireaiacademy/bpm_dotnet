using beeproperties.admin.webapi.repository.Entities;

namespace beeproperties.admin.webapi.repository.Commands.Interface
{
    public interface IAgencyCommandRepository
    {
        Task<AgencyEntity> AddAsync(AgencyEntity entity, CancellationToken cancellationToken = default);
        Task<AgencyEntity?> UpdateAsync(AgencyEntity entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}

