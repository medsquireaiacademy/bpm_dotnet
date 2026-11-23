using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.repository.Query.Interface;
using beeproperties.admin.webapi.services.Commands;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.services.Query
{
    public class AuthQueryService : IAuthQueryService
    {
        private readonly IAuthQueryRepository _queryRepository;

        public AuthQueryService(IAuthQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<AdminUserDto?> GetCurrentUserAsync(GetCurrentUserQuery query, CancellationToken cancellationToken = default)
        {
            var user = await _queryRepository.GetUserByUsernameAsync("admin", cancellationToken) ??
                       await _queryRepository.GetUserByIdAsync("usr-admin-01", cancellationToken);

            return user == null ? null : AuthCommandService.MapUserToDto(user);
        }

        public async Task<List<ActivityDto>> GetRecentActivitiesAsync(GetRecentActivitiesQuery query, CancellationToken cancellationToken = default)
        {
            var entities = await _queryRepository.GetRecentActivitiesAsync(query.Limit, query.Type, cancellationToken);

            return entities.Select(e =>
            {
                var diff = DateTime.UtcNow - e.Timestamp;
                string timeText;
                if (diff.TotalMinutes < 1) timeText = "Just now";
                else if (diff.TotalMinutes < 60) timeText = $"{(int)diff.TotalMinutes}m ago";
                else if (diff.TotalHours < 24) timeText = $"{(int)diff.TotalHours}h ago";
                else timeText = $"{(int)diff.TotalDays}d ago";

                return new ActivityDto
                {
                    Id = e.Id,
                    Type = e.Type,
                    Title = e.Title,
                    Description = e.Description,
                    Time = timeText,
                    Icon = e.Icon,
                    Timestamp = e.Timestamp.ToString("o")
                };
            }).ToList();
        }
    }
}
