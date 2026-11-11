using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.Handler.Command
{
    public class UpdateCommunityHandler : IUpdateCommunityHandler
    {
        private readonly ICommunityCommandService _commandService;

        public UpdateCommunityHandler(ICommunityCommandService commandService)
        {
            _commandService = commandService;
        }

        public Task<CommunityDetailResponse?> HandleAsync(UpdateCommunityCommand command, CancellationToken cancellationToken = default)
        {
            return _commandService.UpdateCommunityAsync(command, cancellationToken);
        }
    }
}

