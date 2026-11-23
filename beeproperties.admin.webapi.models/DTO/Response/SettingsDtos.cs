namespace beeproperties.admin.webapi.models.DTO.Response
{
    public class ServiceCategoryDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
    }

    public class MasterLookupsDto
    {
        public List<string> ServiceCategories { get; set; } = new();
        public List<string> PropertyTypes { get; set; } = new();
        public List<string> PropertyStatuses { get; set; } = new();
        public List<string> BusinessTypes { get; set; } = new();
        public List<string> AgentTypes { get; set; } = new();
        public List<string> PreferredContactMethods { get; set; } = new();
        public List<string> RegistrationStatuses { get; set; } = new();
        public List<string> AssignmentTypes { get; set; } = new();
        public List<string> AssignmentStatuses { get; set; } = new();
        public List<string> ApprovalDecisions { get; set; } = new();
        public List<string> PropertyDocumentCategories { get; set; } = new();
        public List<string> AgencyDocumentTypes { get; set; } = new();
    }

    public class AppPreferencesDto
    {
        public string Theme { get; set; } = "light";
        public bool EmailNotifications { get; set; } = true;
        public bool AutoAssignCompatibleAgency { get; set; } = false;
        public string DefaultScheduledSlot { get; set; } = "09:00 - 12:00 (Morning Slot)";
        public string? PreferencesJson { get; set; }
    }
}
