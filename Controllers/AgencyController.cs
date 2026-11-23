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

        [HttpPost("{id}/review")]
        [ProducesResponseType(typeof(SuccessResponse<AgencyDetailResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ReviewRegistration(
            [FromRoute] string id,
            [FromBody] ReviewAgencyRegistrationCommand command,
            [FromServices] IReviewAgencyRegistrationHandler handler,
            CancellationToken cancellationToken)
        {
            command.AgencyId = id;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null) return NotFound(new SuccessResponse("Agency not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse<AgencyDetailResponse>(data, $"Agency registration decision recorded: {command.Decision}"));
        }

        [HttpGet("{id}/personnel")]
        [ProducesResponseType(typeof(SuccessResponse<List<AgencyPersonnelDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPersonnel(
            [FromRoute] string id,
            [FromQuery] string? status,
            [FromQuery] string? shift,
            [FromServices] IGetAgencyPersonnelHandler handler,
            CancellationToken cancellationToken)
        {
            var data = await handler.HandleAsync(new GetAgencyPersonnelQuery(id, status, shift), cancellationToken);
            return Ok(new SuccessResponse<List<AgencyPersonnelDto>>(data, "Agency personnel retrieved."));
        }

        [HttpPost("{id}/personnel")]
        [ProducesResponseType(typeof(SuccessResponse<AgencyPersonnelDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddPersonnel(
            [FromRoute] string id,
            [FromBody] CreateAgencyPersonnelCommand command,
            [FromServices] ICreateAgencyPersonnelHandler handler,
            CancellationToken cancellationToken)
        {
            command.AgencyId = id;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null) return NotFound(new SuccessResponse("Agency not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse<AgencyPersonnelDto>(data, "Personnel added successfully."));
        }

        [HttpPut("{id}/personnel/{personnelId}")]
        [ProducesResponseType(typeof(SuccessResponse<AgencyPersonnelDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePersonnel(
            [FromRoute] string id,
            [FromRoute] string personnelId,
            [FromBody] UpdateAgencyPersonnelCommand command,
            [FromServices] IUpdateAgencyPersonnelHandler handler,
            CancellationToken cancellationToken)
        {
            command.AgencyId = id;
            command.PersonnelId = personnelId;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null) return NotFound(new SuccessResponse("Personnel or Agency not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse<AgencyPersonnelDto>(data, "Personnel updated successfully."));
        }

        [HttpDelete("{id}/personnel/{personnelId}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePersonnel(
            [FromRoute] string id,
            [FromRoute] string personnelId,
            [FromServices] IDeleteAgencyPersonnelHandler handler,
            CancellationToken cancellationToken)
        {
            var success = await handler.HandleAsync(new DeleteAgencyPersonnelCommand { AgencyId = id, PersonnelId = personnelId }, cancellationToken);
            if (!success) return NotFound(new SuccessResponse("Personnel not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse("Personnel removed successfully."));
        }

        [HttpGet("{id}/property-assignments")]
        [ProducesResponseType(typeof(SuccessResponse<List<AgencyPropertyAssignmentDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPropertyAssignments(
            [FromRoute] string id,
            [FromServices] IGetAgencyPropertyAssignmentsHandler handler,
            CancellationToken cancellationToken)
        {
            var data = await handler.HandleAsync(new GetAgencyPropertyAssignmentsQuery(id), cancellationToken);
            return Ok(new SuccessResponse<List<AgencyPropertyAssignmentDto>>(data, "Property assignments retrieved."));
        }

        [HttpPost("{id}/property-assignments")]
        [ProducesResponseType(typeof(SuccessResponse<AgencyPropertyAssignmentDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> RequestPropertyAssignment(
            [FromRoute] string id,
            [FromBody] RequestPropertyAssignmentCommand command,
            [FromServices] IRequestPropertyAssignmentHandler handler,
            CancellationToken cancellationToken)
        {
            command.AgencyId = id;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null) return NotFound(new SuccessResponse("Agency not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse<AgencyPropertyAssignmentDto>(data, "Property assignment requested."));
        }

        [HttpPost("{id}/property-assignments/{assignmentId}/review")]
        [ProducesResponseType(typeof(SuccessResponse<AgencyPropertyAssignmentDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ReviewPropertyAssignment(
            [FromRoute] string id,
            [FromRoute] string assignmentId,
            [FromBody] ReviewPropertyAssignmentCommand command,
            [FromServices] IReviewPropertyAssignmentHandler handler,
            CancellationToken cancellationToken)
        {
            command.AgencyId = id;
            command.AssignmentId = assignmentId;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null) return NotFound(new SuccessResponse("Assignment or Agency not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse<AgencyPropertyAssignmentDto>(data, $"Assignment reviewed: {command.Decision}"));
        }

        [HttpDelete("{id}/property-assignments/{assignmentId}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> RevokePropertyAssignment(
            [FromRoute] string id,
            [FromRoute] string assignmentId,
            [FromServices] IRevokePropertyAssignmentHandler handler,
            CancellationToken cancellationToken)
        {
            var success = await handler.HandleAsync(new RevokePropertyAssignmentCommand { AgencyId = id, AssignmentId = assignmentId }, cancellationToken);
            if (!success) return NotFound(new SuccessResponse("Assignment not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse("Property assignment revoked successfully."));
        }

        [HttpPost("{id}/services")]
        [ProducesResponseType(typeof(SuccessResponse<AgencyServiceDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddService(
            [FromRoute] string id,
            [FromBody] CreateAgencyServiceCommand command,
            [FromServices] ICreateAgencyServiceHandler handler,
            CancellationToken cancellationToken)
        {
            command.AgencyId = id;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null) return NotFound(new SuccessResponse("Agency not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse<AgencyServiceDto>(data, "Service added successfully."));
        }

        [HttpDelete("{id}/services/{serviceId}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteService(
            [FromRoute] string id,
            [FromRoute] string serviceId,
            [FromServices] IDeleteAgencyServiceHandler handler,
            CancellationToken cancellationToken)
        {
            var success = await handler.HandleAsync(new DeleteAgencyServiceCommand { AgencyId = id, ServiceId = serviceId }, cancellationToken);
            if (!success) return NotFound(new SuccessResponse("Service not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse("Service deleted successfully."));
        }

        [HttpPost("{id}/documents")]
        [ProducesResponseType(typeof(SuccessResponse<AgencyDocumentDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddDocument(
            [FromRoute] string id,
            [FromBody] AddAgencyDocumentCommand command,
            [FromServices] IAddAgencyDocumentHandler handler,
            CancellationToken cancellationToken)
        {
            command.AgencyId = id;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null) return NotFound(new SuccessResponse("Agency not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse<AgencyDocumentDto>(data, "Document added successfully."));
        }

        [HttpDelete("{id}/documents/{documentId}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteDocument(
            [FromRoute] string id,
            [FromRoute] string documentId,
            [FromServices] IDeleteAgencyDocumentHandler handler,
            CancellationToken cancellationToken)
        {
            var success = await handler.HandleAsync(new DeleteAgencyDocumentCommand { AgencyId = id, DocumentId = documentId }, cancellationToken);
            if (!success) return NotFound(new SuccessResponse("Document not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse("Document deleted successfully."));
        }

        [HttpGet("{id}/approval-history")]
        [ProducesResponseType(typeof(SuccessResponse<List<AgencyApprovalHistoryDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetApprovalHistory(
            [FromRoute] string id,
            [FromServices] IGetAgencyApprovalHistoryHandler handler,
            CancellationToken cancellationToken)
        {
            var data = await handler.HandleAsync(new GetAgencyApprovalHistoryQuery(id), cancellationToken);
            return Ok(new SuccessResponse<List<AgencyApprovalHistoryDto>>(data, "Approval history retrieved."));
        }
    }
}

