using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.repository.Query.Interface;
using beeproperties.admin.webapi.services.Commands;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.services.Query
{
    public class SettingsQueryService : ISettingsQueryService
    {
        private readonly ISettingsQueryRepository _queryRepository;

        public SettingsQueryService(ISettingsQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<List<ServiceCategoryDto>> GetAllServiceCategoriesAsync(GetAllServiceCategoriesQuery query, CancellationToken cancellationToken = default)
        {
            var entities = await _queryRepository.GetServiceCategoriesAsync(cancellationToken);
            var filtered = entities.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(query.Status))
            {
                filtered = filtered.Where(c => c.Status.Equals(query.Status.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                var term = query.SearchTerm.Trim().ToLowerInvariant();
                filtered = filtered.Where(c => c.Name.ToLowerInvariant().Contains(term) || c.Description.ToLowerInvariant().Contains(term));
            }

            return filtered.Select(SettingsCommandService.MapCategoryToDto).ToList();
        }

        public async Task<MasterLookupsDto> GetMasterLookupsAsync(GetMasterLookupsQuery query, CancellationToken cancellationToken = default)
        {
            var dto = new MasterLookupsDto
            {
                ServiceCategories = await _queryRepository.GetLookupsAsync("ServiceCategories", cancellationToken),
                PropertyTypes = await _queryRepository.GetLookupsAsync("PropertyTypes", cancellationToken),
                PropertyStatuses = await _queryRepository.GetLookupsAsync("PropertyStatuses", cancellationToken),
                BusinessTypes = await _queryRepository.GetLookupsAsync("BusinessTypes", cancellationToken),
                AgentTypes = await _queryRepository.GetLookupsAsync("AgentTypes", cancellationToken),
                PreferredContactMethods = await _queryRepository.GetLookupsAsync("PreferredContactMethods", cancellationToken),
                RegistrationStatuses = await _queryRepository.GetLookupsAsync("RegistrationStatuses", cancellationToken),
                AssignmentTypes = await _queryRepository.GetLookupsAsync("AssignmentTypes", cancellationToken),
                AssignmentStatuses = await _queryRepository.GetLookupsAsync("AssignmentStatuses", cancellationToken),
                ApprovalDecisions = await _queryRepository.GetLookupsAsync("ApprovalDecisions", cancellationToken),
                PropertyDocumentCategories = await _queryRepository.GetLookupsAsync("PropertyDocumentCategories", cancellationToken),
                AgencyDocumentTypes = await _queryRepository.GetLookupsAsync("AgencyDocumentTypes", cancellationToken)
            };

            return dto;
        }

        public async Task<AppPreferencesDto> GetAppPreferencesAsync(GetAppPreferencesQuery query, CancellationToken cancellationToken = default)
        {
            var prefs = await _queryRepository.GetPreferencesAsync(cancellationToken);
            return SettingsCommandService.MapPreferencesToDto(prefs);
        }
    }
}
