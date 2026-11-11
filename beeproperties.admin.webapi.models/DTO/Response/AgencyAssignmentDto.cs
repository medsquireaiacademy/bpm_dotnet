namespace beeproperties.admin.webapi.models.DTO.Response
{
    public class AgencyAssignmentDto
    {
        public string Id { get; set; } = string.Empty;
        public string Property { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public List<string> Personnel { get; set; } = new();
        public string Shift { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}

