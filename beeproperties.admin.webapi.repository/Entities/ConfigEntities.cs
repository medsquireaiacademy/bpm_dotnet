namespace beeproperties.admin.webapi.repository.Entities
{
    public class ServiceCategoryEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public class MasterLookupEntity
    {
        public string Id { get; set; } = string.Empty;
        public string LookupGroup { get; set; } = string.Empty;
        public string LookupValue { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class AppPreferencesEntity
    {
        public string Id { get; set; } = string.Empty;
        public string? AdminUserId { get; set; }
        public string Theme { get; set; } = "light";
        public bool EmailNotifications { get; set; } = true;
        public bool AutoAssignCompatibleAgency { get; set; } = false;
        public string DefaultScheduledSlot { get; set; } = "09:00 - 12:00 (Morning Slot)";
        public string? PreferencesJson { get; set; }
    }
}
