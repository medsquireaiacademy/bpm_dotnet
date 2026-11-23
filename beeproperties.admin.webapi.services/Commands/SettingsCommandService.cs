using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.repository.Commands.Interface;
using beeproperties.admin.webapi.repository.Entities;
using beeproperties.admin.webapi.repository.Query.Interface;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.services.Commands
{
    public class SettingsCommandService : ISettingsCommandService
    {
        private readonly ISettingsCommandRepository _commandRepository;
        private readonly ISettingsQueryRepository _queryRepository;

        public SettingsCommandService(
            ISettingsCommandRepository commandRepository,
            ISettingsQueryRepository queryRepository)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
        }

        public async Task<ServiceCategoryDto> CreateServiceCategoryAsync(CreateServiceCategoryCommand command, CancellationToken cancellationToken = default)
        {
            var entity = new ServiceCategoryEntity
            {
                Id = $"cat-{DateTime.UtcNow.Ticks}",
                Name = command.Name.Trim(),
                Description = command.Description ?? string.Empty,
                Status = command.Status ?? "Active"
            };

            var created = await _commandRepository.AddCategoryAsync(entity, cancellationToken);
            return MapCategoryToDto(created);
        }

        public async Task<ServiceCategoryDto?> UpdateServiceCategoryAsync(UpdateServiceCategoryCommand command, CancellationToken cancellationToken = default)
        {
            var existing = await _queryRepository.GetServiceCategoryByIdAsync(command.Id, cancellationToken);
            if (existing == null) return null;

            existing.Name = command.Name.Trim();
            existing.Description = command.Description ?? string.Empty;
            existing.Status = command.Status;

            var updated = await _commandRepository.UpdateCategoryAsync(existing, cancellationToken);
            return updated == null ? null : MapCategoryToDto(updated);
        }

        public async Task<bool> ToggleServiceCategoryStatusAsync(ToggleServiceCategoryStatusCommand command, CancellationToken cancellationToken = default)
        {
            var existing = await _queryRepository.GetServiceCategoryByIdAsync(command.Id, cancellationToken);
            if (existing == null) return false;

            existing.Status = existing.Status.Equals("Active", StringComparison.OrdinalIgnoreCase) ? "Inactive" : "Active";
            await _commandRepository.UpdateCategoryAsync(existing, cancellationToken);
            return true;
        }

        public Task<bool> DeleteServiceCategoryAsync(DeleteServiceCategoryCommand command, CancellationToken cancellationToken = default)
        {
            return _commandRepository.DeleteCategoryAsync(command.Id, cancellationToken);
        }

        public async Task<bool> UpdateMasterLookupAsync(UpdateMasterLookupCommand command, CancellationToken cancellationToken = default)
        {
            await _commandRepository.SetLookupValuesAsync(command.LookupGroup, command.Values, cancellationToken);
            return true;
        }

        public async Task<AppPreferencesDto> UpdateAppPreferencesAsync(UpdateAppPreferencesCommand command, CancellationToken cancellationToken = default)
        {
            var entity = new AppPreferencesEntity
            {
                Theme = command.Theme ?? "light",
                EmailNotifications = command.EmailNotifications ?? true,
                AutoAssignCompatibleAgency = command.AutoAssignCompatibleAgency ?? false,
                DefaultScheduledSlot = command.DefaultScheduledSlot ?? "09:00 - 12:00 (Morning Slot)",
                PreferencesJson = command.PreferencesJson
            };

            var updated = await _commandRepository.UpdatePreferencesAsync(entity, cancellationToken);
            return MapPreferencesToDto(updated);
        }

        public async Task<bool> ResetDataAsync(ResetDataCommand command, CancellationToken cancellationToken = default)
        {
            if (!command.ConfirmReset) return false;
            await _commandRepository.ResetDataAsync(cancellationToken);
            return true;
        }

        public static ServiceCategoryDto MapCategoryToDto(ServiceCategoryEntity c)
        {
            return new ServiceCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Status = c.Status
            };
        }

        public static AppPreferencesDto MapPreferencesToDto(AppPreferencesEntity p)
        {
            return new AppPreferencesDto
            {
                Theme = p.Theme,
                EmailNotifications = p.EmailNotifications,
                AutoAssignCompatibleAgency = p.AutoAssignCompatibleAgency,
                DefaultScheduledSlot = p.DefaultScheduledSlot,
                PreferencesJson = p.PreferencesJson
            };
        }
    }
}
