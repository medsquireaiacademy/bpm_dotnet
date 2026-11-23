using Microsoft.AspNetCore.Mvc;
using beeproperties.admin.webapi.Handler.Command;
using beeproperties.admin.webapi.Handler.Query;
using beeproperties.admin.webapi.models.DTO.Helper;
using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.models.Models.Query;

namespace beeproperties.admin.webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(typeof(SuccessResponse<AuthResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(
            [FromBody] LoginCommand command,
            [FromServices] ILoginHandler handler,
            CancellationToken cancellationToken = default)
        {
            var response = await handler.HandleAsync(command, cancellationToken);
            if (response == null || !response.Success)
            {
                return Unauthorized(new SuccessResponse("Invalid username or password.", StatusCodes.Status401Unauthorized) { Success = false });
            }
            return Ok(new SuccessResponse<AuthResponseDto>(response, "Login successful."));
        }

        [HttpGet("me")]
        [ProducesResponseType(typeof(SuccessResponse<AdminUserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurrentUser(
            [FromServices] IGetCurrentUserHandler handler,
            CancellationToken cancellationToken = default)
        {
            var user = await handler.HandleAsync(new GetCurrentUserQuery(), cancellationToken);
            if (user == null)
            {
                return NotFound(new SuccessResponse("Current admin user not found.", StatusCodes.Status404NotFound) { Success = false });
            }
            return Ok(new SuccessResponse<AdminUserDto>(user, "Profile retrieved successfully."));
        }

        [HttpPut("profile")]
        [ProducesResponseType(typeof(SuccessResponse<AdminUserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProfile(
            [FromBody] UpdateAdminProfileCommand command,
            [FromServices] IUpdateAdminProfileHandler handler,
            CancellationToken cancellationToken = default)
        {
            var updated = await handler.HandleAsync(command, cancellationToken);
            if (updated == null)
            {
                return NotFound(new SuccessResponse("Admin profile not found.", StatusCodes.Status404NotFound) { Success = false });
            }
            return Ok(new SuccessResponse<AdminUserDto>(updated, "Profile updated successfully."));
        }

        [HttpPost("change-password")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangeAdminPasswordCommand command,
            [FromServices] IChangeAdminPasswordHandler handler,
            CancellationToken cancellationToken = default)
        {
            var success = await handler.HandleAsync(command, cancellationToken);
            if (!success)
            {
                return BadRequest(new SuccessResponse("Current password is incorrect.", StatusCodes.Status400BadRequest) { Success = false });
            }
            return Ok(new SuccessResponse("Password changed successfully."));
        }

        [HttpGet("activities")]
        [ProducesResponseType(typeof(SuccessResponse<List<ActivityDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActivities(
            [FromServices] IGetRecentActivitiesHandler handler,
            [FromQuery] int limit = 10,
            [FromQuery] string? type = null,
            CancellationToken cancellationToken = default)
        {
            var data = await handler.HandleAsync(new GetRecentActivitiesQuery { Limit = limit, Type = type }, cancellationToken);
            return Ok(new SuccessResponse<List<ActivityDto>>(data, "Recent activities retrieved."));
        }

        [HttpPost("activities")]
        [ProducesResponseType(typeof(SuccessResponse<ActivityDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateActivity(
            [FromBody] CreateActivityCommand command,
            [FromServices] ICreateActivityHandler handler,
            CancellationToken cancellationToken = default)
        {
            var data = await handler.HandleAsync(command, cancellationToken);
            return Ok(new SuccessResponse<ActivityDto>(data, "Activity recorded successfully."));
        }
    }
}
