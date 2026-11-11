using FluentValidation;
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
    public class AgencyController : ControllerBase
    {
        /// <summary>
        /// Retrieves all agencies with optional search and filters.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(SuccessResponse<List<AgencyResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromServices] IGetAllAgenciesHandler handler,
            [FromQuery] string? searchTerm,
            [FromQuery] string? category,
            [FromQuery] string? agreementStatus,
            [FromQuery] string? availabilityStatus,
            [FromQuery] string? city,
            [FromQuery] string? state,
            CancellationToken cancellationToken)
        {
            var query = new GetAllAgenciesQuery
            {
                SearchTerm = searchTerm,
                Category = category,
                AgreementStatus = agreementStatus,
                AvailabilityStatus = availabilityStatus,
                City = city,
                State = state
            };

            var data = await handler.HandleAsync(query, cancellationToken);
            return Ok(new SuccessResponse<List<AgencyResponse>>(data, "Agencies retrieved successfully."));
        }

        /// <summary>
        /// Retrieves full details of a specific agency by ID (including personnel, assignments, services, documents, and approval history).
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SuccessResponse<AgencyDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(
            [FromRoute] string id,
            [FromServices] IGetAgencyByIdHandler handler,
            [FromServices] IValidator<GetAgencyByIdQuery> validator,
            CancellationToken cancellationToken)
        {
            var query = new GetAgencyByIdQuery(id);
            var validation = await validator.ValidateAsync(query, cancellationToken);
            if (!validation.IsValid)
            {
                return BadRequest(validation.ToDictionary());
            }

            var agency = await handler.HandleAsync(query, cancellationToken);
            if (agency == null)
            {
                return NotFound(new SuccessResponse($"Agency with ID '{id}' was not found.", StatusCodes.Status404NotFound)
                {
                    Success = false
                });
            }

            return Ok(new SuccessResponse<AgencyDetailResponse>(agency, "Agency retrieved successfully."));
        }

        /// <summary>
        /// Creates a new agency.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponse<AgencyDetailResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody] CreateAgencyCommand command,
            [FromServices] ICreateAgencyHandler handler,
            [FromServices] IValidator<CreateAgencyCommand> validator,
            CancellationToken cancellationToken)
        {
            var validation = await validator.ValidateAsync(command, cancellationToken);
            if (!validation.IsValid)
            {
                return BadRequest(validation.ToDictionary());
            }

            var created = await handler.HandleAsync(command, cancellationToken);
            var response = new SuccessResponse<AgencyDetailResponse>(created, "Agency created successfully.", StatusCodes.Status201Created);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
        }

        /// <summary>
        /// Updates an existing agency.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SuccessResponse<AgencyDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute] string id,
            [FromBody] UpdateAgencyCommand command,
            [FromServices] IUpdateAgencyHandler handler,
            [FromServices] IValidator<UpdateAgencyCommand> validator,
            CancellationToken cancellationToken)
        {
            if (!string.Equals(id, command.Id, StringComparison.OrdinalIgnoreCase))
            {
                command.Id = id;
            }

            var validation = await validator.ValidateAsync(command, cancellationToken);
            if (!validation.IsValid)
            {
                return BadRequest(validation.ToDictionary());
            }

            var updated = await handler.HandleAsync(command, cancellationToken);
            if (updated == null)
            {
                return NotFound(new SuccessResponse($"Agency with ID '{id}' was not found.", StatusCodes.Status404NotFound)
                {
                    Success = false
                });
            }

            return Ok(new SuccessResponse<AgencyDetailResponse>(updated, "Agency updated successfully."));
        }

        /// <summary>
        /// Deletes an agency by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            [FromRoute] string id,
            [FromServices] IDeleteAgencyHandler handler,
            CancellationToken cancellationToken)
        {
            var command = new DeleteAgencyCommand(id);
            var success = await handler.HandleAsync(command, cancellationToken);
            if (!success)
            {
                return NotFound(new SuccessResponse($"Agency with ID '{id}' was not found.", StatusCodes.Status404NotFound)
                {
                    Success = false
                });
            }

            return Ok(new SuccessResponse("Agency deleted successfully."));
        }
    }
}

