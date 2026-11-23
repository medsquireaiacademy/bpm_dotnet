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
    public class SecurityController : ControllerBase
    {
        [HttpGet("deployments")]
        [ProducesResponseType(typeof(SuccessResponse<List<SecurityDeploymentDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDeployments(
            [FromServices] IGetAllSecurityDeploymentsHandler handler,
            [FromQuery] string? propertyId,
            [FromQuery] string? agencyId,
            [FromQuery] string? status,
            CancellationToken cancellationToken = default)
        {
            var query = new GetAllSecurityDeploymentsQuery
            {
                PropertyId = propertyId,
                AgencyId = agencyId,
                Status = status
            };

            var data = await handler.HandleAsync(query, cancellationToken);
            return Ok(new SuccessResponse<List<SecurityDeploymentDto>>(data, "Security deployments retrieved successfully."));
        }

        [HttpGet("deployments/{id}")]
        [ProducesResponseType(typeof(SuccessResponse<SecurityDeploymentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDeploymentById(
            [FromRoute] string id,
            [FromServices] IGetSecurityDeploymentByIdHandler handler,
            CancellationToken cancellationToken = default)
        {
            var data = await handler.HandleAsync(new GetSecurityDeploymentByIdQuery(id), cancellationToken);
            if (data == null)
            {
                return NotFound(new SuccessResponse($"Deployment with ID '{id}' was not found.", StatusCodes.Status404NotFound) { Success = false });
            }
            return Ok(new SuccessResponse<SecurityDeploymentDto>(data, "Security deployment details retrieved."));
        }

        [HttpPost("deployments")]
        [ProducesResponseType(typeof(SuccessResponse<SecurityDeploymentDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateDeployment(
            [FromBody] CreateSecurityDeploymentCommand command,
            [FromServices] ICreateSecurityDeploymentHandler handler,
            CancellationToken cancellationToken = default)
        {
            var data = await handler.HandleAsync(command, cancellationToken);
            return Ok(new SuccessResponse<SecurityDeploymentDto>(data, "Security deployment created successfully."));
        }

        [HttpPatch("deployments/{id}/status")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateDeploymentStatus(
            [FromRoute] string id,
            [FromBody] UpdateSecurityDeploymentStatusCommand command,
            [FromServices] IUpdateSecurityDeploymentStatusHandler handler,
            CancellationToken cancellationToken = default)
        {
            command.Id = id;
            var success = await handler.HandleAsync(command, cancellationToken);
            if (!success)
            {
                return NotFound(new SuccessResponse($"Deployment with ID '{id}' was not found.", StatusCodes.Status404NotFound) { Success = false });
            }
            return Ok(new SuccessResponse($"Security deployment status updated to '{command.Status}'."));
        }

        [HttpDelete("deployments/{id}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteDeployment(
            [FromRoute] string id,
            [FromServices] IDeleteSecurityDeploymentHandler handler,
            CancellationToken cancellationToken = default)
        {
            var success = await handler.HandleAsync(new DeleteSecurityDeploymentCommand { Id = id }, cancellationToken);
            if (!success)
            {
                return NotFound(new SuccessResponse($"Deployment with ID '{id}' was not found.", StatusCodes.Status404NotFound) { Success = false });
            }
            return Ok(new SuccessResponse("Security deployment deleted successfully."));
        }
    }
}
