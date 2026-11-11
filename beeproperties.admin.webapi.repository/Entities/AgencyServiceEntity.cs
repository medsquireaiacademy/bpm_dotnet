namespace beeproperties.admin.webapi.repository.Entities
{
    public class AgencyServiceEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string OtherServiceName { get; set; } = string.Empty;
        public string CoverageArea { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public string Certifications { get; set; } = string.Empty;
        public string Availability { get; set; } = string.Empty;
    }
}

