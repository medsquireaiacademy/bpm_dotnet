using beeproperties.admin.webapi.repository.Entities;

namespace beeproperties.admin.webapi.repository.Query.Interface
{
    public interface IAgencyQueryRepository
    {
        Task<List<AgencyEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<AgencyEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    }
}

