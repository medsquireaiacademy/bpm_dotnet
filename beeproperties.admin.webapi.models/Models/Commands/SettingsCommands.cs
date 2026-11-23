namespace beeproperties.admin.webapi.models.Models.Commands
{
    public class CreateServiceCategoryCommand
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = "Active";
    }

    public class UpdateServiceCategoryCommand : CreateServiceCategoryCommand
    {
        public string Id { get; set; } = string.Empty;
    }

    public class ToggleServiceCategoryStatusCommand
    {
        public string Id { get; set; } = string.Empty;
    }

    public class DeleteServiceCategoryCommand
    {
        public string Id { get; set; } = string.Empty;
    }

    public class UpdateMasterLookupCommand
    {
        public string LookupGroup { get; set; } = string.Empty;
        public List<string> Values { get; set; } = new();
    }

    public class UpdateAppPreferencesCommand
    {
        public string Theme { get; set; } = "light";
        public bool? EmailNotifications { get; set; }
        public bool? AutoAssignCompatibleAgency { get; set; }
        public string? DefaultScheduledSlot { get; set; }
        public string? PreferencesJson { get; set; }
    }

    public class ResetDataCommand
    {
        public bool ConfirmReset { get; set; } = true;
    }
}
