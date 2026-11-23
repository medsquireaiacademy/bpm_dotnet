namespace beeproperties.admin.webapi.models.Models.Commands
{
    public class CreateSecurityDeploymentCommand
    {
        public string? Id { get; set; }
        public string ApartmentId { get; set; } = string.Empty;
        public string AgencyId { get; set; } = string.Empty;
        public string BlockName { get; set; } = "All Perimeter Gates & Main Entrance";
        public string Shift { get; set; } = "Morning Shift (06:00 - 14:00)";
        public int RequiredGuards { get; set; } = 2;
        public int AllocatedGuards { get; set; } = 2;
        public List<string> PersonnelNames { get; set; } = new();
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }

    public class UpdateSecurityDeploymentStatusCommand
    {
        public string Id { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
    }

    public class DeleteSecurityDeploymentCommand
    {
        public string Id { get; set; } = string.Empty;
    }
}
