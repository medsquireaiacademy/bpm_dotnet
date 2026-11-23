using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.Handler.Command
{
    public interface ICreateMaintenanceTicketHandler
    {
        Task<MaintenanceTicketDetailDto> HandleAsync(CreateMaintenanceTicketCommand command, CancellationToken cancellationToken = default);
    }

    public class CreateMaintenanceTicketHandler : ICreateMaintenanceTicketHandler
    {
        private readonly IMaintenanceCommandService _service;
        public CreateMaintenanceTicketHandler(IMaintenanceCommandService service) => _service = service;
        public Task<MaintenanceTicketDetailDto> HandleAsync(CreateMaintenanceTicketCommand command, CancellationToken cancellationToken = default) =>
            _service.CreateTicketAsync(command, cancellationToken);
    }

    public interface IUpdateMaintenanceTicketHandler
    {
        Task<MaintenanceTicketDetailDto?> HandleAsync(UpdateMaintenanceTicketCommand command, CancellationToken cancellationToken = default);
    }

    public class UpdateMaintenanceTicketHandler : IUpdateMaintenanceTicketHandler
    {
        private readonly IMaintenanceCommandService _service;
        public UpdateMaintenanceTicketHandler(IMaintenanceCommandService service) => _service = service;
        public Task<MaintenanceTicketDetailDto?> HandleAsync(UpdateMaintenanceTicketCommand command, CancellationToken cancellationToken = default) =>
            _service.UpdateTicketAsync(command, cancellationToken);
    }

    public interface IUpdateMaintenanceStatusHandler
    {
        Task<bool> HandleAsync(UpdateMaintenanceStatusCommand command, CancellationToken cancellationToken = default);
    }

    public class UpdateMaintenanceStatusHandler : IUpdateMaintenanceStatusHandler
    {
        private readonly IMaintenanceCommandService _service;
        public UpdateMaintenanceStatusHandler(IMaintenanceCommandService service) => _service = service;
        public Task<bool> HandleAsync(UpdateMaintenanceStatusCommand command, CancellationToken cancellationToken = default) =>
            _service.UpdateStatusAsync(command, cancellationToken);
    }

    public interface IAllocateAgencyToMaintenanceHandler
    {
        Task<MaintenanceTicketDetailDto?> HandleAsync(AllocateAgencyToMaintenanceCommand command, CancellationToken cancellationToken = default);
    }

    public class AllocateAgencyToMaintenanceHandler : IAllocateAgencyToMaintenanceHandler
    {
        private readonly IMaintenanceCommandService _service;
        public AllocateAgencyToMaintenanceHandler(IMaintenanceCommandService service) => _service = service;
        public Task<MaintenanceTicketDetailDto?> HandleAsync(AllocateAgencyToMaintenanceCommand command, CancellationToken cancellationToken = default) =>
            _service.AllocateAgencyAsync(command, cancellationToken);
    }

    public interface IAddMaintenanceFeedbackHandler
    {
        Task<MaintenanceFeedbackDto?> HandleAsync(AddMaintenanceFeedbackCommand command, CancellationToken cancellationToken = default);
    }

    public class AddMaintenanceFeedbackHandler : IAddMaintenanceFeedbackHandler
    {
        private readonly IMaintenanceCommandService _service;
        public AddMaintenanceFeedbackHandler(IMaintenanceCommandService service) => _service = service;
        public Task<MaintenanceFeedbackDto?> HandleAsync(AddMaintenanceFeedbackCommand command, CancellationToken cancellationToken = default) =>
            _service.AddFeedbackAsync(command, cancellationToken);
    }

    public interface IDeleteMaintenanceTicketHandler
    {
        Task<bool> HandleAsync(DeleteMaintenanceTicketCommand command, CancellationToken cancellationToken = default);
    }

    public class DeleteMaintenanceTicketHandler : IDeleteMaintenanceTicketHandler
    {
        private readonly IMaintenanceCommandService _service;
        public DeleteMaintenanceTicketHandler(IMaintenanceCommandService service) => _service = service;
        public Task<bool> HandleAsync(DeleteMaintenanceTicketCommand command, CancellationToken cancellationToken = default) =>
            _service.DeleteTicketAsync(command, cancellationToken);
    }
}
