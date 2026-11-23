using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.Handler.Command
{
    public interface ICreateServiceCategoryHandler
    {
        Task<ServiceCategoryDto> HandleAsync(CreateServiceCategoryCommand command, CancellationToken cancellationToken = default);
    }

    public class CreateServiceCategoryHandler : ICreateServiceCategoryHandler
    {
        private readonly ISettingsCommandService _service;
        public CreateServiceCategoryHandler(ISettingsCommandService service) => _service = service;
        public Task<ServiceCategoryDto> HandleAsync(CreateServiceCategoryCommand command, CancellationToken cancellationToken = default) =>
            _service.CreateServiceCategoryAsync(command, cancellationToken);
    }

    public interface IUpdateServiceCategoryHandler
    {
        Task<ServiceCategoryDto?> HandleAsync(UpdateServiceCategoryCommand command, CancellationToken cancellationToken = default);
    }

    public class UpdateServiceCategoryHandler : IUpdateServiceCategoryHandler
    {
        private readonly ISettingsCommandService _service;
        public UpdateServiceCategoryHandler(ISettingsCommandService service) => _service = service;
        public Task<ServiceCategoryDto?> HandleAsync(UpdateServiceCategoryCommand command, CancellationToken cancellationToken = default) =>
            _service.UpdateServiceCategoryAsync(command, cancellationToken);
    }

    public interface IToggleServiceCategoryStatusHandler
    {
        Task<bool> HandleAsync(ToggleServiceCategoryStatusCommand command, CancellationToken cancellationToken = default);
    }

    public class ToggleServiceCategoryStatusHandler : IToggleServiceCategoryStatusHandler
    {
        private readonly ISettingsCommandService _service;
        public ToggleServiceCategoryStatusHandler(ISettingsCommandService service) => _service = service;
        public Task<bool> HandleAsync(ToggleServiceCategoryStatusCommand command, CancellationToken cancellationToken = default) =>
            _service.ToggleServiceCategoryStatusAsync(command, cancellationToken);
    }

    public interface IDeleteServiceCategoryHandler
    {
        Task<bool> HandleAsync(DeleteServiceCategoryCommand command, CancellationToken cancellationToken = default);
    }

    public class DeleteServiceCategoryHandler : IDeleteServiceCategoryHandler
    {
        private readonly ISettingsCommandService _service;
        public DeleteServiceCategoryHandler(ISettingsCommandService service) => _service = service;
        public Task<bool> HandleAsync(DeleteServiceCategoryCommand command, CancellationToken cancellationToken = default) =>
            _service.DeleteServiceCategoryAsync(command, cancellationToken);
    }

    public interface IUpdateMasterLookupHandler
    {
        Task<bool> HandleAsync(UpdateMasterLookupCommand command, CancellationToken cancellationToken = default);
    }

    public class UpdateMasterLookupHandler : IUpdateMasterLookupHandler
    {
        private readonly ISettingsCommandService _service;
        public UpdateMasterLookupHandler(ISettingsCommandService service) => _service = service;
        public Task<bool> HandleAsync(UpdateMasterLookupCommand command, CancellationToken cancellationToken = default) =>
            _service.UpdateMasterLookupAsync(command, cancellationToken);
    }

    public interface IUpdateAppPreferencesHandler
    {
        Task<AppPreferencesDto> HandleAsync(UpdateAppPreferencesCommand command, CancellationToken cancellationToken = default);
    }

    public class UpdateAppPreferencesHandler : IUpdateAppPreferencesHandler
    {
        private readonly ISettingsCommandService _service;
        public UpdateAppPreferencesHandler(ISettingsCommandService service) => _service = service;
        public Task<AppPreferencesDto> HandleAsync(UpdateAppPreferencesCommand command, CancellationToken cancellationToken = default) =>
            _service.UpdateAppPreferencesAsync(command, cancellationToken);
    }

    public interface IResetDataHandler
    {
        Task<bool> HandleAsync(ResetDataCommand command, CancellationToken cancellationToken = default);
    }

    public class ResetDataHandler : IResetDataHandler
    {
        private readonly ISettingsCommandService _service;
        public ResetDataHandler(ISettingsCommandService service) => _service = service;
        public Task<bool> HandleAsync(ResetDataCommand command, CancellationToken cancellationToken = default) =>
            _service.ResetDataAsync(command, cancellationToken);
    }
}
