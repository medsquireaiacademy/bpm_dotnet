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
    public class CommunityController : ControllerBase
    {
        /// <summary>
        /// Retrieves all communities with optional search and filters.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(SuccessResponse<List<CommunityResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromServices] IGetAllCommunitiesHandler handler,
            [FromQuery] string? searchTerm,
            [FromQuery] string? city,
            [FromQuery] string? state,
            CancellationToken cancellationToken)
        {
            var query = new GetAllCommunitiesQuery
            {
                SearchTerm = searchTerm,
                City = city,
                State = state
            };

            var data = await handler.HandleAsync(query, cancellationToken);
            return Ok(new SuccessResponse<List<CommunityResponse>>(data, "Communities retrieved successfully."));
        }

        /// <summary>
        /// Retrieves full details of a specific community by ID (including amenities, apartments, and flats).
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SuccessResponse<CommunityDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(
            [FromRoute] string id,
            [FromServices] IGetCommunityByIdHandler handler,
            [FromServices] IValidator<GetCommunityByIdQuery> validator,
            CancellationToken cancellationToken)
        {
            var query = new GetCommunityByIdQuery(id);
            var validation = await validator.ValidateAsync(query, cancellationToken);
            if (!validation.IsValid)
            {
                return BadRequest(validation.ToDictionary());
            }

            var community = await handler.HandleAsync(query, cancellationToken);
            if (community == null)
            {
                return NotFound(new SuccessResponse($"Community with ID '{id}' was not found.", StatusCodes.Status404NotFound)
                {
                    Success = false
                });
            }

            return Ok(new SuccessResponse<CommunityDetailResponse>(community, "Community retrieved successfully."));
        }

        /// <summary>
        /// Creates a new community.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponse<CommunityDetailResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody] CreateCommunityCommand command,
            [FromServices] ICreateCommunityHandler handler,
            [FromServices] IValidator<CreateCommunityCommand> validator,
            CancellationToken cancellationToken)
        {
            var validation = await validator.ValidateAsync(command, cancellationToken);
            if (!validation.IsValid)
            {
                return BadRequest(validation.ToDictionary());
            }

            var created = await handler.HandleAsync(command, cancellationToken);
            var response = new SuccessResponse<CommunityDetailResponse>(created, "Community created successfully.", StatusCodes.Status201Created);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
        }

        /// <summary>
        /// Updates an existing community.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SuccessResponse<CommunityDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute] string id,
            [FromBody] UpdateCommunityCommand command,
            [FromServices] IUpdateCommunityHandler handler,
            [FromServices] IValidator<UpdateCommunityCommand> validator,
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
                return NotFound(new SuccessResponse($"Community with ID '{id}' was not found.", StatusCodes.Status404NotFound)
                {
                    Success = false
                });
            }

            return Ok(new SuccessResponse<CommunityDetailResponse>(updated, "Community updated successfully."));
        }

        /// <summary>
        /// Deletes a community by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            [FromRoute] string id,
            [FromServices] IDeleteCommunityHandler handler,
            CancellationToken cancellationToken)
        {
            var command = new DeleteCommunityCommand(id);
            var success = await handler.HandleAsync(command, cancellationToken);
            if (!success)
            {
                return NotFound(new SuccessResponse($"Community with ID '{id}' was not found.", StatusCodes.Status404NotFound)
                {
                    Success = false
                });
            }

            return Ok(new SuccessResponse("Community deleted successfully."));
        }
    }
}
