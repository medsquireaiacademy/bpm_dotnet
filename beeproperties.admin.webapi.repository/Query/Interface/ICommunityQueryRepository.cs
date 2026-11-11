using beeproperties.admin.webapi.repository.Entities;

namespace beeproperties.admin.webapi.repository.Query.Interface
{
    public interface ICommunityQueryRepository
    {
        Task<List<CommunityEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CommunityEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    }
}

