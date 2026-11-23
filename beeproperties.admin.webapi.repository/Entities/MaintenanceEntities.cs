namespace beeproperties.admin.webapi.repository.Entities
{
    public class MaintenanceRequestEntity
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
        public string PropertyId { get => ApartmentId; set => ApartmentId = value; }
        public string ApartmentName { get; set; } = string.Empty;
        public string PropertyName { get => ApartmentName; set => ApartmentName = value; }
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
        public string? ResolutionNotes { get; set; }
        public string? CompletedDate { get; set; }
        public List<MaintenanceFeedbackEntity> AdminFeedback { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public class MaintenanceFeedbackEntity
    {
        public string Id { get; set; } = string.Empty;
        public string MaintenanceRequestId { get; set; } = string.Empty;
        public string AdminName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Timestamp { get; set; } = string.Empty;
        public string Status { get; set; } = "Sent";
    }

    public class MaintenanceAssignedPersonnelEntity
    {
        public string Id { get; set; } = string.Empty;
        public string MaintenanceRequestId { get; set; } = string.Empty;
        public string PersonnelName { get; set; } = string.Empty;
        public string AssignedDate { get; set; } = string.Empty;
    }
}
