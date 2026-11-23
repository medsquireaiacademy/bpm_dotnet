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
    public class MaintenanceController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(SuccessResponse<PagedResult<MaintenanceTicketDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromServices] IGetAllMaintenanceRequestsHandler handler,
            [FromQuery] string? propertyId,
            [FromQuery] string? blockId,
            [FromQuery] string? category,
            [FromQuery] string? priority,
            [FromQuery] string? status,
            [FromQuery] string? searchTerm,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var query = new GetAllMaintenanceRequestsQuery
            {
                PropertyId = propertyId,
                BlockId = blockId,
                Category = category,
                Priority = priority,
                Status = status,
                SearchTerm = searchTerm,
                Page = page,
                PageSize = pageSize
            };

            var data = await handler.HandleAsync(query, cancellationToken);
            return Ok(new SuccessResponse<PagedResult<MaintenanceTicketDto>>(data, "Maintenance tickets retrieved successfully."));
        }

        [HttpGet("compatible-agencies")]
        [ProducesResponseType(typeof(SuccessResponse<List<CompatibleAgencyDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompatibleAgencies(
            [FromServices] IGetCompatibleAgenciesHandler handler,
            [FromQuery] string category,
            [FromQuery] string? propertyId,
            CancellationToken cancellationToken = default)
        {
            var query = new GetCompatibleAgenciesQuery { Category = category, PropertyId = propertyId };
            var data = await handler.HandleAsync(query, cancellationToken);
            return Ok(new SuccessResponse<List<CompatibleAgencyDto>>(data, "Compatible agencies retrieved."));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SuccessResponse<MaintenanceTicketDetailDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            [FromRoute] string id,
            [FromServices] IGetMaintenanceRequestByIdHandler handler,
            CancellationToken cancellationToken = default)
        {
            var data = await handler.HandleAsync(new GetMaintenanceRequestByIdQuery(id), cancellationToken);
            if (data == null)
            {
                return NotFound(new SuccessResponse($"Maintenance ticket with ID '{id}' was not found.", StatusCodes.Status404NotFound) { Success = false });
            }
            return Ok(new SuccessResponse<MaintenanceTicketDetailDto>(data, "Maintenance ticket details retrieved."));
        }

        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponse<MaintenanceTicketDetailDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            [FromBody] CreateMaintenanceTicketCommand command,
            [FromServices] ICreateMaintenanceTicketHandler handler,
            CancellationToken cancellationToken = default)
        {
            var data = await handler.HandleAsync(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = data.Id }, new SuccessResponse<MaintenanceTicketDetailDto>(data, "Maintenance ticket created successfully."));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SuccessResponse<MaintenanceTicketDetailDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            [FromRoute] string id,
            [FromBody] UpdateMaintenanceTicketCommand command,
            [FromServices] IUpdateMaintenanceTicketHandler handler,
            CancellationToken cancellationToken = default)
        {
            command.Id = id;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null)
            {
                return NotFound(new SuccessResponse($"Ticket with ID '{id}' was not found.", StatusCodes.Status404NotFound) { Success = false });
            }
            return Ok(new SuccessResponse<MaintenanceTicketDetailDto>(data, "Ticket updated successfully."));
        }

        [HttpPatch("{id}/status")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus(
            [FromRoute] string id,
            [FromBody] UpdateMaintenanceStatusCommand command,
            [FromServices] IUpdateMaintenanceStatusHandler handler,
            CancellationToken cancellationToken = default)
        {
            command.Id = id;
            var success = await handler.HandleAsync(command, cancellationToken);
            if (!success)
            {
                return NotFound(new SuccessResponse($"Ticket with ID '{id}' was not found.", StatusCodes.Status404NotFound) { Success = false });
            }
            return Ok(new SuccessResponse($"Maintenance ticket status updated to '{command.Status}'."));
        }

        [HttpPost("{id}/allocate")]
        [ProducesResponseType(typeof(SuccessResponse<MaintenanceTicketDetailDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AllocateAgency(
            [FromRoute] string id,
            [FromBody] AllocateAgencyToMaintenanceCommand command,
            [FromServices] IAllocateAgencyToMaintenanceHandler handler,
            CancellationToken cancellationToken = default)
        {
            command.MaintenanceId = id;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null)
            {
                return NotFound(new SuccessResponse($"Ticket with ID '{id}' was not found.", StatusCodes.Status404NotFound) { Success = false });
            }
            return Ok(new SuccessResponse<MaintenanceTicketDetailDto>(data, "Agency allocated to maintenance ticket successfully."));
        }

        [HttpPost("{id}/feedback")]
        [ProducesResponseType(typeof(SuccessResponse<MaintenanceFeedbackDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddFeedback(
            [FromRoute] string id,
            [FromBody] AddMaintenanceFeedbackCommand command,
            [FromServices] IAddMaintenanceFeedbackHandler handler,
            CancellationToken cancellationToken = default)
        {
            command.MaintenanceId = id;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null)
            {
                return NotFound(new SuccessResponse($"Ticket with ID '{id}' was not found.", StatusCodes.Status404NotFound) { Success = false });
            }
            return Ok(new SuccessResponse<MaintenanceFeedbackDto>(data, "Feedback added successfully."));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(
            [FromRoute] string id,
            [FromServices] IDeleteMaintenanceTicketHandler handler,
            CancellationToken cancellationToken = default)
        {
            var success = await handler.HandleAsync(new DeleteMaintenanceTicketCommand { Id = id }, cancellationToken);
            if (!success)
            {
                return NotFound(new SuccessResponse($"Ticket with ID '{id}' was not found.", StatusCodes.Status404NotFound) { Success = false });
            }
            return Ok(new SuccessResponse("Maintenance ticket deleted successfully."));
        }
    }
}
