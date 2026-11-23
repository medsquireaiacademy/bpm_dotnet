namespace beeproperties.admin.webapi.models.Models.Query
{
    public class GetAllServiceCategoriesQuery
    {
        public string? Status { get; set; }
        public string? SearchTerm { get; set; }
    }

    public class GetMasterLookupsQuery
    {
        public string? LookupGroup { get; set; }
    }

    public class GetAppPreferencesQuery
    {
    }
}
