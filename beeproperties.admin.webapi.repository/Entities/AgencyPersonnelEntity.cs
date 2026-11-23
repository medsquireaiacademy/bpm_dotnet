namespace beeproperties.admin.webapi.repository.Entities
{
    public class AgencyPersonnelEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CurrentShift { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
    }
}

