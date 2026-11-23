using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.repository.Entities;
using beeproperties.admin.webapi.repository.Query.Interface;
using beeproperties.admin.webapi.services.Commands;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.services.Query
{
    public class MaintenanceQueryService : IMaintenanceQueryService
    {
        private readonly IMaintenanceQueryRepository _queryRepository;
        private readonly IAgencyQueryRepository _agencyQueryRepository;

        public MaintenanceQueryService(
            IMaintenanceQueryRepository queryRepository,
            IAgencyQueryRepository agencyQueryRepository)
        {
            _queryRepository = queryRepository;
            _agencyQueryRepository = agencyQueryRepository;
        }

        public async Task<PagedResult<MaintenanceTicketDto>> GetAllRequestsAsync(GetAllMaintenanceRequestsQuery query, CancellationToken cancellationToken = default)
        {
            var entities = await _queryRepository.GetAllAsync(cancellationToken);
            var filtered = entities.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(query.PropertyId))
            {
                filtered = filtered.Where(r => r.PropertyId.Equals(query.PropertyId.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(query.BlockId))
            {
                filtered = filtered.Where(r => !string.IsNullOrEmpty(r.BlockId) && r.BlockId.Equals(query.BlockId.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                filtered = filtered.Where(r => r.Category.Equals(query.Category.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(query.Priority))
            {
                filtered = filtered.Where(r => r.Priority.Equals(query.Priority.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(query.Status))
            {
                filtered = filtered.Where(r => r.Status.Equals(query.Status.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                var term = query.SearchTerm.Trim().ToLowerInvariant();
                filtered = filtered.Where(r =>
                    r.Title.ToLowerInvariant().Contains(term) ||
                    r.TicketNumber.ToLowerInvariant().Contains(term) ||
                    r.Description.ToLowerInvariant().Contains(term) ||
                    r.PropertyName.ToLowerInvariant().Contains(term) ||
                    (r.FlatNumber != null && r.FlatNumber.ToLowerInvariant().Contains(term)) ||
                    (r.AssignedAgencyName != null && r.AssignedAgencyName.ToLowerInvariant().Contains(term)));
            }

            var totalCount = filtered.Count();
            var page = query.Page > 0 ? query.Page : 1;
            var pageSize = query.PageSize > 0 ? query.PageSize : 20;

            var items = filtered
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(MaintenanceCommandService.MapToDto)
                .ToList();

            return new PagedResult<MaintenanceTicketDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<MaintenanceTicketDetailDto?> GetRequestByIdAsync(GetMaintenanceRequestByIdQuery query, CancellationToken cancellationToken = default)
        {
            var entity = await _queryRepository.GetByIdAsync(query.Id, cancellationToken);
            return entity == null ? null : MaintenanceCommandService.MapToDetailDto(entity);
        }

        public async Task<List<CompatibleAgencyDto>> GetCompatibleAgenciesAsync(GetCompatibleAgenciesQuery query, CancellationToken cancellationToken = default)
        {
            var agencies = await _agencyQueryRepository.GetAllAsync(cancellationToken);

            var compatible = agencies.Where(a =>
                a.AgreementStatus.Equals("Active", StringComparison.OrdinalIgnoreCase) &&
                (string.IsNullOrWhiteSpace(query.Category) ||
                 a.Category.Contains(query.Category, StringComparison.OrdinalIgnoreCase) ||
                 (a.Categories != null && a.Categories.Any(c => c.Contains(query.Category, StringComparison.OrdinalIgnoreCase))) ||
                 (a.Services != null && a.Services.Any(s => s.Category.Contains(query.Category, StringComparison.OrdinalIgnoreCase))))
            );

            return compatible.Select(a => new CompatibleAgencyDto
            {
                Id = a.Id,
                Name = a.Name,
                Category = a.Category,
                AvailablePersonnel = a.AvailablePersonnel > 0 ? a.AvailablePersonnel : a.Personnel.Count(p => p.Status == "Available"),
                AvailableStaffNames = a.Personnel.Where(p => p.Status == "Available").Select(p => p.Name).ToList()
            }).ToList();
        }
    }
}
