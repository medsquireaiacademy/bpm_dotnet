namespace beeproperties.admin.webapi.models.Models.Query
{
    public class GetDashboardMetricsQuery
    {
    }

    public class GetServiceRequestChartsQuery
    {
        public string? PropertyId { get; set; }
        public string? Period { get; set; }
    }

    public class GetPendingApprovalsQuery
    {
    }

    public class GetRecentActivitiesQuery
    {
        public int Limit { get; set; } = 10;
        public string? Type { get; set; }
    }

    public class GetCurrentUserQuery
    {
    }
}
