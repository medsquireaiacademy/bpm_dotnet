namespace beeproperties.admin.webapi.models.Models.Query
{
    public class GetAllMaintenanceRequestsQuery
    {
        public string? PropertyId { get; set; }
        public string? BlockId { get; set; }
        public string? Category { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
        public string? SearchTerm { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class GetMaintenanceRequestByIdQuery
    {
        public string Id { get; set; }

        public GetMaintenanceRequestByIdQuery(string id)
        {
            Id = id;
        }
    }

    public class GetCompatibleAgenciesQuery
    {
        public string? PropertyId { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
