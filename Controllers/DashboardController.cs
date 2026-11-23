using Microsoft.AspNetCore.Mvc;
using beeproperties.admin.webapi.Handler.Query;
using beeproperties.admin.webapi.models.DTO.Helper;
using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;

namespace beeproperties.admin.webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        [HttpGet("metrics")]
        [ProducesResponseType(typeof(SuccessResponse<DashboardMetricsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMetrics(
            [FromServices] IGetDashboardMetricsHandler handler,
            CancellationToken cancellationToken = default)
        {
            var data = await handler.HandleAsync(new GetDashboardMetricsQuery(), cancellationToken);
            return Ok(new SuccessResponse<DashboardMetricsDto>(data, "Dashboard metrics retrieved."));
        }

        [HttpGet("charts")]
        [ProducesResponseType(typeof(SuccessResponse<ServiceRequestChartsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCharts(
            [FromServices] IGetServiceRequestChartsHandler handler,
            [FromQuery] string? propertyId,
            [FromQuery] string? period,
            CancellationToken cancellationToken = default)
        {
            var data = await handler.HandleAsync(new GetServiceRequestChartsQuery { PropertyId = propertyId, Period = period }, cancellationToken);
            return Ok(new SuccessResponse<ServiceRequestChartsDto>(data, "Service request chart data retrieved."));
        }

        [HttpGet("pending-approvals")]
        [ProducesResponseType(typeof(SuccessResponse<PendingApprovalsSummaryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPendingApprovals(
            [FromServices] IGetPendingApprovalsHandler handler,
            CancellationToken cancellationToken = default)
        {
            var data = await handler.HandleAsync(new GetPendingApprovalsQuery(), cancellationToken);
            return Ok(new SuccessResponse<PendingApprovalsSummaryDto>(data, "Pending approvals retrieved."));
        }
    }
}
