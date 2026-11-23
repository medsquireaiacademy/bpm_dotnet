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
    public class SettingsController : ControllerBase
    {
        [HttpGet("categories")]
        [ProducesResponseType(typeof(SuccessResponse<List<ServiceCategoryDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories(
            [FromServices] IGetAllServiceCategoriesHandler handler,
            [FromQuery] string? status,
            [FromQuery] string? searchTerm,
            CancellationToken cancellationToken = default)
        {
            var query = new GetAllServiceCategoriesQuery { Status = status, SearchTerm = searchTerm };
            var data = await handler.HandleAsync(query, cancellationToken);
            return Ok(new SuccessResponse<List<ServiceCategoryDto>>(data, "Service categories retrieved."));
        }

        [HttpPost("categories")]
        [ProducesResponseType(typeof(SuccessResponse<ServiceCategoryDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCategory(
            [FromBody] CreateServiceCategoryCommand command,
            [FromServices] ICreateServiceCategoryHandler handler,
            CancellationToken cancellationToken = default)
        {
            var data = await handler.HandleAsync(command, cancellationToken);
            return Ok(new SuccessResponse<ServiceCategoryDto>(data, "Service category created successfully."));
        }

        [HttpPut("categories/{id}")]
        [ProducesResponseType(typeof(SuccessResponse<ServiceCategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCategory(
            [FromRoute] string id,
            [FromBody] UpdateServiceCategoryCommand command,
            [FromServices] IUpdateServiceCategoryHandler handler,
            CancellationToken cancellationToken = default)
        {
            command.Id = id;
            var data = await handler.HandleAsync(command, cancellationToken);
            if (data == null) return NotFound(new SuccessResponse("Category not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse<ServiceCategoryDto>(data, "Service category updated successfully."));
        }

        [HttpPatch("categories/{id}/toggle-status")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> ToggleCategoryStatus(
            [FromRoute] string id,
            [FromServices] IToggleServiceCategoryStatusHandler handler,
            CancellationToken cancellationToken = default)
        {
            var success = await handler.HandleAsync(new ToggleServiceCategoryStatusCommand { Id = id }, cancellationToken);
            if (!success) return NotFound(new SuccessResponse("Category not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse("Category status toggled successfully."));
        }

        [HttpDelete("categories/{id}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCategory(
            [FromRoute] string id,
            [FromServices] IDeleteServiceCategoryHandler handler,
            CancellationToken cancellationToken = default)
        {
            var success = await handler.HandleAsync(new DeleteServiceCategoryCommand { Id = id }, cancellationToken);
            if (!success) return NotFound(new SuccessResponse("Category not found.", StatusCodes.Status404NotFound) { Success = false });
            return Ok(new SuccessResponse("Category deleted successfully."));
        }

        [HttpGet("lookups")]
        [ProducesResponseType(typeof(SuccessResponse<MasterLookupsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLookups(
            [FromServices] IGetMasterLookupsHandler handler,
            [FromQuery] string? lookupGroup,
            CancellationToken cancellationToken = default)
        {
            var data = await handler.HandleAsync(new GetMasterLookupsQuery { LookupGroup = lookupGroup }, cancellationToken);
            return Ok(new SuccessResponse<MasterLookupsDto>(data, "Master lookups retrieved."));
        }

        [HttpPut("lookups/{group}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateLookup(
            [FromRoute] string group,
            [FromBody] List<string> values,
            [FromServices] IUpdateMasterLookupHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new UpdateMasterLookupCommand { LookupGroup = group, Values = values };
            await handler.HandleAsync(command, cancellationToken);
            return Ok(new SuccessResponse($"Lookup '{group}' updated successfully."));
        }

        [HttpGet("preferences")]
        [ProducesResponseType(typeof(SuccessResponse<AppPreferencesDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPreferences(
            [FromServices] IGetAppPreferencesHandler handler,
            CancellationToken cancellationToken = default)
        {
            var data = await handler.HandleAsync(new GetAppPreferencesQuery(), cancellationToken);
            return Ok(new SuccessResponse<AppPreferencesDto>(data, "App preferences retrieved."));
        }

        [HttpPut("preferences")]
        [ProducesResponseType(typeof(SuccessResponse<AppPreferencesDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePreferences(
            [FromBody] UpdateAppPreferencesCommand command,
            [FromServices] IUpdateAppPreferencesHandler handler,
            CancellationToken cancellationToken = default)
        {
            var data = await handler.HandleAsync(command, cancellationToken);
            return Ok(new SuccessResponse<AppPreferencesDto>(data, "Preferences saved successfully."));
        }

        [HttpPost("reset")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetData(
            [FromBody] ResetDataCommand command,
            [FromServices] IResetDataHandler handler,
            CancellationToken cancellationToken = default)
        {
            var success = await handler.HandleAsync(command, cancellationToken);
            if (!success) return BadRequest(new SuccessResponse("Reset was not confirmed.", StatusCodes.Status400BadRequest) { Success = false });
            return Ok(new SuccessResponse("All application data reset to initial seed state."));
        }
    }
}
