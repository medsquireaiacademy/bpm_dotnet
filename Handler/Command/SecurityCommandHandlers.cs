using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.Handler.Command
{
    public interface ICreateSecurityDeploymentHandler
    {
        Task<SecurityDeploymentDto> HandleAsync(CreateSecurityDeploymentCommand command, CancellationToken cancellationToken = default);
    }

    public class CreateSecurityDeploymentHandler : ICreateSecurityDeploymentHandler
    {
        private readonly ISecurityCommandService _service;
        public CreateSecurityDeploymentHandler(ISecurityCommandService service) => _service = service;
        public Task<SecurityDeploymentDto> HandleAsync(CreateSecurityDeploymentCommand command, CancellationToken cancellationToken = default) =>
            _service.CreateDeploymentAsync(command, cancellationToken);
    }

    public interface IUpdateSecurityDeploymentStatusHandler
    {
        Task<bool> HandleAsync(UpdateSecurityDeploymentStatusCommand command, CancellationToken cancellationToken = default);
    }

    public class UpdateSecurityDeploymentStatusHandler : IUpdateSecurityDeploymentStatusHandler
    {
        private readonly ISecurityCommandService _service;
        public UpdateSecurityDeploymentStatusHandler(ISecurityCommandService service) => _service = service;
        public Task<bool> HandleAsync(UpdateSecurityDeploymentStatusCommand command, CancellationToken cancellationToken = default) =>
            _service.UpdateStatusAsync(command, cancellationToken);
    }

    public interface IDeleteSecurityDeploymentHandler
    {
        Task<bool> HandleAsync(DeleteSecurityDeploymentCommand command, CancellationToken cancellationToken = default);
    }

    public class DeleteSecurityDeploymentHandler : IDeleteSecurityDeploymentHandler
    {
        private readonly ISecurityCommandService _service;
        public DeleteSecurityDeploymentHandler(ISecurityCommandService service) => _service = service;
        public Task<bool> HandleAsync(DeleteSecurityDeploymentCommand command, CancellationToken cancellationToken = default) =>
            _service.DeleteDeploymentAsync(command, cancellationToken);
    }
}
