using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;

namespace beeproperties.admin.webapi.Handler.Command
{
    public interface ICreateCommunityHandler
    {
        Task<CommunityDetailResponse> HandleAsync(CreateCommunityCommand command, CancellationToken cancellationToken = default);
    }
}

