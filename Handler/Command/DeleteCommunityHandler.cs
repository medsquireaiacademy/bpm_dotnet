using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.Handler.Command
{
    public class DeleteCommunityHandler : IDeleteCommunityHandler
    {
        private readonly ICommunityCommandService _commandService;

        public DeleteCommunityHandler(ICommunityCommandService commandService)
        {
            _commandService = commandService;
        }

        public Task<bool> HandleAsync(DeleteCommunityCommand command, CancellationToken cancellationToken = default)
        {
            return _commandService.DeleteCommunityAsync(command, cancellationToken);
        }
    }
}

