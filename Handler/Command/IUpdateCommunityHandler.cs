using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;

namespace beeproperties.admin.webapi.Handler.Command
{
    public interface IUpdateCommunityHandler
    {
        Task<CommunityDetailResponse?> HandleAsync(UpdateCommunityCommand command, CancellationToken cancellationToken = default);
    }
}

