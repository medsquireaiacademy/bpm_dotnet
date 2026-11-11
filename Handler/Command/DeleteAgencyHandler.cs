using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.Handler.Command
{
    public class DeleteAgencyHandler : IDeleteAgencyHandler
    {
        private readonly IAgencyCommandService _commandService;

        public DeleteAgencyHandler(IAgencyCommandService commandService)
        {
            _commandService = commandService;
        }

        public Task<bool> HandleAsync(DeleteAgencyCommand command, CancellationToken cancellationToken = default)
        {
            return _commandService.DeleteAgencyAsync(command, cancellationToken);
        }
    }
}

