using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.repository.Query.Interface;
using beeproperties.admin.webapi.services.Commands;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.services.Query
{
    public class SecurityQueryService : ISecurityQueryService
    {
        private readonly ISecurityQueryRepository _queryRepository;

        public SecurityQueryService(ISecurityQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<List<SecurityDeploymentDto>> GetAllDeploymentsAsync(GetAllSecurityDeploymentsQuery query, CancellationToken cancellationToken = default)
        {
            var entities = await _queryRepository.GetAllAsync(cancellationToken);
            var filtered = entities.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(query.PropertyId))
            {
                filtered = filtered.Where(d => d.PropertyId.Equals(query.PropertyId.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(query.AgencyId))
            {
                filtered = filtered.Where(d => d.AgencyId.Equals(query.AgencyId.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(query.Status))
            {
                filtered = filtered.Where(d => d.Status.Equals(query.Status.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            return filtered.Select(SecurityCommandService.MapToDto).ToList();
        }

        public async Task<SecurityDeploymentDto?> GetDeploymentByIdAsync(GetSecurityDeploymentByIdQuery query, CancellationToken cancellationToken = default)
        {
            var entity = await _queryRepository.GetByIdAsync(query.Id, cancellationToken);
            return entity == null ? null : SecurityCommandService.MapToDto(entity);
        }
    }
}
