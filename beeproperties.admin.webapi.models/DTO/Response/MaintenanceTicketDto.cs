namespace beeproperties.admin.webapi.models.DTO.Response
{
    public class MaintenanceTicketDto
    {
        public string Id { get; set; } = string.Empty;
        public string TicketNumber { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Priority { get; set; } = "Medium";
        public string Status { get; set; } = "Reported";
        public string ReportedDate { get; set; } = string.Empty;
        public string ApartmentId { get; set; } = string.Empty;
        public string ApartmentName { get; set; } = string.Empty;
        public string? BlockId { get; set; }
        public string? BlockName { get; set; }
        public string? FloorId { get; set; }
        public string? FlatNumber { get; set; }
        public string? LocationDescription { get; set; }
        public string? AssignedAgencyId { get; set; }
        public string? AssignedAgencyName { get; set; }
        public List<string> AssignedPersonnel { get; set; } = new();
        public string? ScheduledDate { get; set; }
        public string? ScheduledTime { get; set; }
    }

    public class MaintenanceTicketDetailDto : MaintenanceTicketDto
    {
        public string? ResolutionNotes { get; set; }
        public string? CompletedDate { get; set; }
        public List<MaintenanceFeedbackDto> AdminFeedback { get; set; } = new();
    }

    public class MaintenanceFeedbackDto
    {
        public string Id { get; set; } = string.Empty;
        public string AdminName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Timestamp { get; set; } = string.Empty;
        public string Status { get; set; } = "Sent";
    }

    public class MaintenanceAssignedPersonnelDto
    {
        public string Id { get; set; } = string.Empty;
        public string MaintenanceRequestId { get; set; } = string.Empty;
        public string PersonnelName { get; set; } = string.Empty;
        public string AssignedDate { get; set; } = string.Empty;
    }

    public class CompatibleAgencyDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int AvailablePersonnel { get; set; }
        public List<string> AvailableStaffNames { get; set; } = new();
    }
}
