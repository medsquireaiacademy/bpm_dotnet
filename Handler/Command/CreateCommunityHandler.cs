using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.Handler.Command
{
    public class CreateCommunityHandler : ICreateCommunityHandler
    {
        private readonly ICommunityCommandService _commandService;

        public CreateCommunityHandler(ICommunityCommandService commandService)
        {
            _commandService = commandService;
        }

        public Task<CommunityDetailResponse> HandleAsync(CreateCommunityCommand command, CancellationToken cancellationToken = default)
        {
            return _commandService.CreateCommunityAsync(command, cancellationToken);
        }
    }
}

