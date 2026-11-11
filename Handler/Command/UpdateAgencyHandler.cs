using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.Handler.Command
{
    public class UpdateAgencyHandler : IUpdateAgencyHandler
    {
        private readonly IAgencyCommandService _commandService;

        public UpdateAgencyHandler(IAgencyCommandService commandService)
        {
            _commandService = commandService;
        }

        public Task<AgencyDetailResponse?> HandleAsync(UpdateAgencyCommand command, CancellationToken cancellationToken = default)
        {
            return _commandService.UpdateAgencyAsync(command, cancellationToken);
        }
    }
}

