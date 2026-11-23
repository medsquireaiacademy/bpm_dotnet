namespace beeproperties.admin.webapi.models.Models.Commands
{
    public class CreateMaintenanceTicketCommand
    {
        public string? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string ApartmentId { get; set; } = string.Empty;
        public string ApartmentName { get; set; } = string.Empty;
        public string? BlockId { get; set; }
        public string? BlockName { get; set; }
        public string? FloorId { get; set; }
        public string? FlatNumber { get; set; }
        public string? LocationDescription { get; set; }
        public string Priority { get; set; } = "Medium";
    }

    public class UpdateMaintenanceTicketCommand
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Priority { get; set; } = "Medium";
    }

    public class UpdateMaintenanceStatusCommand
    {
        public string Id { get; set; } = string.Empty;
        public string Status { get; set; } = "In Progress";
    }

    public class AllocateAgencyToMaintenanceCommand
    {
        public string MaintenanceId { get; set; } = string.Empty;
        public string AgencyId { get; set; } = string.Empty;
        public List<string> PersonnelNames { get; set; } = new();
        public string ScheduledDate { get; set; } = string.Empty;
        public string ScheduledTimeSlot { get; set; } = string.Empty;
    }

    public class AddMaintenanceFeedbackCommand
    {
        public string MaintenanceId { get; set; } = string.Empty;
        public string AdminName { get; set; } = "Super Admin";
        public string Message { get; set; } = string.Empty;
        public string Status { get; set; } = "Sent";
    }

    public class DeleteMaintenanceTicketCommand
    {
        public string Id { get; set; } = string.Empty;
    }
}
