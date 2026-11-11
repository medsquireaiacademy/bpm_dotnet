namespace beeproperties.admin.webapi.models.Models.Query
{
    public class GetAllAgenciesQuery
    {
        public string? SearchTerm { get; set; }
        public string? Category { get; set; }
        public string? AgreementStatus { get; set; }
        public string? AvailabilityStatus { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }
}

