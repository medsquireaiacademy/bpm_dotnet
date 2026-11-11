using beeproperties.admin.webapi.repository.Entities;

namespace beeproperties.admin.webapi.repository.Commands.Interface
{
    public interface ICommunityCommandRepository
    {
        Task<CommunityEntity> AddAsync(CommunityEntity entity, CancellationToken cancellationToken = default);
        Task<CommunityEntity?> UpdateAsync(CommunityEntity entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}

