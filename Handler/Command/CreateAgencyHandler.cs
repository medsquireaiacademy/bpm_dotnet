using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.Handler.Command
{
    public class CreateAgencyHandler : ICreateAgencyHandler
    {
        private readonly IAgencyCommandService _commandService;

        public CreateAgencyHandler(IAgencyCommandService commandService)
        {
            _commandService = commandService;
        }

        public Task<AgencyDetailResponse> HandleAsync(CreateAgencyCommand command, CancellationToken cancellationToken = default)
        {
            return _commandService.CreateAgencyAsync(command, cancellationToken);
        }
    }
}

