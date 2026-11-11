using beeproperties.admin.webapi.models.Models.Commands;

namespace beeproperties.admin.webapi.Handler.Command
{
    public interface IDeleteAgencyHandler
    {
        Task<bool> HandleAsync(DeleteAgencyCommand command, CancellationToken cancellationToken = default);
    }
}

