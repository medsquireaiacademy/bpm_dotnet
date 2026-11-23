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
    public class PropertyController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(SuccessResponse<List<PropertyResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromServices] IGetAllPropertiesHandler handler,
            [FromQuery] string? searchTerm,
            [FromQuery] string? status,
            [FromQuery] string? city,
            [FromQuery] string? state,
            [FromQuery] string? propertyType,
            CancellationToken cancellationToken)
        {
            var query = new GetAllPropertiesQuery
            {
                SearchTerm = searchTerm,
                Status = status,
                City = city,
                State = state,
                PropertyType = propertyType
            };

            var data = await handler.HandleAsync(query, cancellationToken);
            return Ok(new SuccessResponse<List<PropertyResponse>>(data, "Properties retrieved successfully."));
        }

        [HttpGet("summary")]
        [ProducesResponseType(typeof(SuccessResponse<PropertySummaryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSummary(
            [FromServices] IGetPropertySummaryHandler handler,
            [FromQuery] string? propertyId,
            CancellationToken cancellationToken)
        {
            var query = new GetPropertySummaryQuery(propertyId);
            var data = await handler.HandleAsync(query, cancellationToken);
            return Ok(new SuccessResponse<PropertySummaryDto>(data, "Property summary metrics retrieved."));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SuccessResponse<PropertyDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            [FromRoute] string id,
            [FromServices] IGetPropertyByIdHandler handler,
            CancellationToken cancellationToken)
        {
            var data = await handler.HandleAsync(new GetPropertyByIdQuery(id), cancellationToken);
            if (data == null)
            {
                return NotFound(new SuccessResponse($"Property with ID '{id}' was not found.", StatusCodes.Status404NotFound) { Success = false });
            }
            return Ok(new SuccessResponse<PropertyDetailResponse>(data, "Property details retrieved."));
        }

        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponse<PropertyDetailResponse>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            [FromBody] CreatePropertyCommand command,
            [FromServices] ICreatePropertyHandler handler,
            CancellationToken cancellationToken)
        {
            var data = await handler.HandleAsync(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = data.Id }, new SuccessResponse<PropertyDetailResponse>(data, "Property created successfully."));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SuccessResponse<PropertyDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            [FromRoute] string id,
            [FromBody] UpdatePropertyCommand command,
            [FromServices] IUpdatePropertyHandler handler,
            CancellationToken cancellationToken)
        {
            command.Id = id;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null)
            {
                return NotFound(new SuccessResponse($"Property with ID '{id}' was not found.", StatusCodes.Status404NotFound) { Success = false });
            }
            return Ok(new SuccessResponse<PropertyDetailResponse>(data, "Property updated successfully."));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            [FromRoute] string id,
            [FromServices] IDeletePropertyHandler handler,
            CancellationToken cancellationToken)
        {
            var success = await handler.HandleAsync(new DeletePropertyCommand(id), cancellationToken);
            if (!success)
            {
                return NotFound(new SuccessResponse($"Property with ID '{id}' was not found.", StatusCodes.Status404NotFound) { Success = false });
            }
            return Ok(new SuccessResponse("Property deleted successfully."));
        }

        [HttpGet("{id}/blocks")]
        [ProducesResponseType(typeof(SuccessResponse<List<PropertyBlockDetailDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBlocks(
            [FromRoute] string id,
            [FromServices] IGetPropertyBlocksHandler handler,
            CancellationToken cancellationToken)
        {
            var data = await handler.HandleAsync(new GetPropertyBlocksQuery(id), cancellationToken);
            return Ok(new SuccessResponse<List<PropertyBlockDetailDto>>(data, "Property blocks retrieved."));
        }

        [HttpPost("{id}/blocks")]
        [ProducesResponseType(typeof(SuccessResponse<PropertyBlockDetailDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateBlock(
            [FromRoute] string id,
            [FromBody] CreateBlockCommand command,
            [FromServices] ICreateBlockHandler handler,
            CancellationToken cancellationToken)
        {
            command.PropertyId = id;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null) return NotFound(new SuccessResponse($"Property with ID '{id}' was not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse<PropertyBlockDetailDto>(data, "Block added successfully."));
        }

        [HttpPut("{id}/blocks/{blockId}")]
        [ProducesResponseType(typeof(SuccessResponse<PropertyBlockDetailDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBlock(
            [FromRoute] string id,
            [FromRoute] string blockId,
            [FromBody] UpdateBlockCommand command,
            [FromServices] IUpdateBlockHandler handler,
            CancellationToken cancellationToken)
        {
            command.PropertyId = id;
            command.BlockId = blockId;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null) return NotFound(new SuccessResponse("Block or Property not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse<PropertyBlockDetailDto>(data, "Block updated successfully."));
        }

        [HttpDelete("{id}/blocks/{blockId}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBlock(
            [FromRoute] string id,
            [FromRoute] string blockId,
            [FromServices] IDeleteBlockHandler handler,
            CancellationToken cancellationToken)
        {
            var success = await handler.HandleAsync(new DeleteBlockCommand { PropertyId = id, BlockId = blockId }, cancellationToken);
            if (!success) return NotFound(new SuccessResponse("Block or Property not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse("Block deleted successfully."));
        }

        [HttpGet("{id}/blocks/{blockId}/floors")]
        [ProducesResponseType(typeof(SuccessResponse<List<FloorDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFloors(
            [FromRoute] string id,
            [FromRoute] string blockId,
            [FromServices] IGetBlockFloorsHandler handler,
            CancellationToken cancellationToken)
        {
            var data = await handler.HandleAsync(new GetBlockFloorsQuery(id, blockId), cancellationToken);
            return Ok(new SuccessResponse<List<FloorDto>>(data, "Floors retrieved."));
        }

        [HttpPost("{id}/blocks/{blockId}/floors")]
        [ProducesResponseType(typeof(SuccessResponse<FloorDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateFloor(
            [FromRoute] string id,
            [FromRoute] string blockId,
            [FromBody] CreateFloorCommand command,
            [FromServices] ICreateFloorHandler handler,
            CancellationToken cancellationToken)
        {
            command.PropertyId = id;
            command.BlockId = blockId;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null) return NotFound(new SuccessResponse("Block or Property not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse<FloorDto>(data, "Floor created successfully."));
        }

        [HttpDelete("{id}/blocks/{blockId}/floors/{floorId}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteFloor(
            [FromRoute] string id,
            [FromRoute] string blockId,
            [FromRoute] string floorId,
            [FromServices] IDeleteFloorHandler handler,
            CancellationToken cancellationToken)
        {
            var success = await handler.HandleAsync(new DeleteFloorCommand { PropertyId = id, BlockId = blockId, FloorId = floorId }, cancellationToken);
            if (!success) return NotFound(new SuccessResponse("Floor not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse("Floor deleted successfully."));
        }

        [HttpGet("{id}/blocks/{blockId}/floors/{floorId}/flats")]
        [ProducesResponseType(typeof(SuccessResponse<List<FlatDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFlats(
            [FromRoute] string id,
            [FromRoute] string blockId,
            [FromRoute] string floorId,
            [FromServices] IGetFloorFlatsHandler handler,
            CancellationToken cancellationToken)
        {
            var data = await handler.HandleAsync(new GetFloorFlatsQuery(id, blockId, floorId), cancellationToken);
            return Ok(new SuccessResponse<List<FlatDto>>(data, "Flats retrieved."));
        }

        [HttpPost("{id}/blocks/{blockId}/floors/{floorId}/flats")]
        [ProducesResponseType(typeof(SuccessResponse<FlatDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateFlat(
            [FromRoute] string id,
            [FromRoute] string blockId,
            [FromRoute] string floorId,
            [FromBody] CreateFlatCommand command,
            [FromServices] ICreateFlatHandler handler,
            CancellationToken cancellationToken)
        {
            command.PropertyId = id;
            command.BlockId = blockId;
            command.FloorId = floorId;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null) return NotFound(new SuccessResponse("Floor or Property not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse<FlatDto>(data, "Flat created successfully."));
        }

        [HttpPut("{id}/blocks/{blockId}/floors/{floorId}/flats/{flatNumber}/resident")]
        [ProducesResponseType(typeof(SuccessResponse<FlatDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateFlatResident(
            [FromRoute] string id,
            [FromRoute] string blockId,
            [FromRoute] string floorId,
            [FromRoute] string flatNumber,
            [FromBody] UpdateFlatResidentCommand command,
            [FromServices] IUpdateFlatResidentHandler handler,
            CancellationToken cancellationToken)
        {
            command.PropertyId = id;
            command.BlockId = blockId;
            command.FloorId = floorId;
            command.FlatNumber = flatNumber;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null) return NotFound(new SuccessResponse("Flat not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse<FlatDto>(data, "Resident updated successfully."));
        }

        [HttpDelete("{id}/blocks/{blockId}/floors/{floorId}/flats/{flatNumber}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteFlat(
            [FromRoute] string id,
            [FromRoute] string blockId,
            [FromRoute] string floorId,
            [FromRoute] string flatNumber,
            [FromServices] IDeleteFlatHandler handler,
            CancellationToken cancellationToken)
        {
            var success = await handler.HandleAsync(new DeleteFlatCommand { PropertyId = id, BlockId = blockId, FloorId = floorId, FlatNumber = flatNumber }, cancellationToken);
            if (!success) return NotFound(new SuccessResponse("Flat not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse("Flat deleted successfully."));
        }

        [HttpGet("{id}/documents")]
        [ProducesResponseType(typeof(SuccessResponse<List<PropertyDocumentDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDocuments(
            [FromRoute] string id,
            [FromQuery] string? category,
            [FromServices] IGetPropertyDocumentsHandler handler,
            CancellationToken cancellationToken)
        {
            var data = await handler.HandleAsync(new GetPropertyDocumentsQuery(id, category), cancellationToken);
            return Ok(new SuccessResponse<List<PropertyDocumentDto>>(data, "Property documents retrieved."));
        }

        [HttpPost("{id}/documents")]
        [ProducesResponseType(typeof(SuccessResponse<PropertyDocumentDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddDocument(
            [FromRoute] string id,
            [FromBody] AddPropertyDocumentCommand command,
            [FromServices] IAddPropertyDocumentHandler handler,
            CancellationToken cancellationToken)
        {
            command.PropertyId = id;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null) return NotFound(new SuccessResponse("Property not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse<PropertyDocumentDto>(data, "Document added successfully."));
        }

        [HttpDelete("{id}/documents/{documentId}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteDocument(
            [FromRoute] string id,
            [FromRoute] string documentId,
            [FromServices] IDeletePropertyDocumentHandler handler,
            CancellationToken cancellationToken)
        {
            var success = await handler.HandleAsync(new DeletePropertyDocumentCommand { PropertyId = id, DocumentId = documentId }, cancellationToken);
            if (!success) return NotFound(new SuccessResponse("Document not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse("Document deleted successfully."));
        }

        [HttpGet("{id}/rwa")]
        [ProducesResponseType(typeof(SuccessResponse<RwaDetailResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRwa(
            [FromRoute] string id,
            [FromServices] IGetPropertyRwaHandler handler,
            CancellationToken cancellationToken)
        {
            var data = await handler.HandleAsync(new GetPropertyRwaQuery(id), cancellationToken);
            return Ok(new SuccessResponse<RwaDetailResponseDto?>(data, "RWA details retrieved."));
        }

        [HttpPost("{id}/rwa")]
        [ProducesResponseType(typeof(SuccessResponse<RwaDetailResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveRwa(
            [FromRoute] string id,
            [FromBody] SaveRwaDetailsCommand command,
            [FromServices] ISaveRwaHandler handler,
            CancellationToken cancellationToken)
        {
            command.PropertyId = id;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null) return NotFound(new SuccessResponse("Property not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse<RwaDetailResponseDto>(data, "RWA details saved."));
        }

        [HttpPatch("{id}/rwa/status")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateRwaStatus(
            [FromRoute] string id,
            [FromBody] UpdateRwaStatusCommand command,
            [FromServices] IUpdateRwaStatusHandler handler,
            CancellationToken cancellationToken)
        {
            command.PropertyId = id;
            var success = await handler.HandleAsync(command, cancellationToken);
            if (!success) return NotFound(new SuccessResponse("Property not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse("RWA status updated."));
        }

        [HttpPost("{id}/rwa/committee")]
        [ProducesResponseType(typeof(SuccessResponse<RwaCommitteeMemberDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddCommitteeMember(
            [FromRoute] string id,
            [FromBody] AddRwaCommitteeMemberCommand command,
            [FromServices] IAddRwaCommitteeMemberHandler handler,
            CancellationToken cancellationToken)
        {
            command.PropertyId = id;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null) return NotFound(new SuccessResponse("Property not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse<RwaCommitteeMemberDto>(data, "Committee member added."));
        }

        [HttpDelete("{id}/rwa/committee/{memberId}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCommitteeMember(
            [FromRoute] string id,
            [FromRoute] string memberId,
            [FromServices] IDeleteRwaCommitteeMemberHandler handler,
            CancellationToken cancellationToken)
        {
            var success = await handler.HandleAsync(new DeleteRwaCommitteeMemberCommand { PropertyId = id, MemberId = memberId }, cancellationToken);
            if (!success) return NotFound(new SuccessResponse("Committee member not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse("Committee member deleted."));
        }
    }
}
