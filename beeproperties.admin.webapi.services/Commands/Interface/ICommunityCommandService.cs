using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;

namespace beeproperties.admin.webapi.services.Commands.Interface
{
    public interface ICommunityCommandService
    {
        Task<CommunityDetailResponse> CreateCommunityAsync(CreateCommunityCommand command, CancellationToken cancellationToken = default);
        Task<CommunityDetailResponse?> UpdateCommunityAsync(UpdateCommunityCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteCommunityAsync(DeleteCommunityCommand command, CancellationToken cancellationToken = default);
    }
}

