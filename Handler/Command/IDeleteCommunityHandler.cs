using beeproperties.admin.webapi.models.Models.Commands;

namespace beeproperties.admin.webapi.Handler.Command
{
    public interface IDeleteCommunityHandler
    {
        Task<bool> HandleAsync(DeleteCommunityCommand command, CancellationToken cancellationToken = default);
    }
}

