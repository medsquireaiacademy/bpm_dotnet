namespace beeproperties.admin.webapi.repository.Entities
{
    public class AgencyApprovalHistoryEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Timestamp { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Decision { get; set; } = string.Empty;
        public string Reviewer { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public string? ChangesRequested { get; set; }
    }
}

