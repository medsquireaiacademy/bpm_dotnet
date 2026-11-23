namespace beeproperties.admin.webapi.models.DTO.Response
{
    public class AdminUserDto
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = "Super Admin";
        public string Initials { get; set; } = "SJ";
        public string Phone { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Token { get; set; }
    }

    public class AuthResponseDto
    {
        public bool Success { get; set; } = true;
        public string Token { get; set; } = string.Empty;
        public AdminUserDto User { get; set; } = new();
        public string Message { get; set; } = "Authentication successful.";
    }

    public class DashboardMetricsDto
    {
        public int TotalApartments { get; set; }
        public int TotalBlocks { get; set; }
        public int TotalFlats { get; set; }
        public int OccupiedFlats { get; set; }
        public int VacantFlats { get; set; }
        public double OccupancyRate { get; set; }
        public int ActiveAgencies { get; set; }
        public int AvailableAgencies { get; set; }
        public int PendingApprovals { get; set; }
        public int ActiveMaintenanceRequests { get; set; }
        public int TotalAllocatedPersonnel { get; set; }
    }

    public class ServiceRequestChartsDto
    {
        public List<ChartStatusBreakdownItem> StatusData { get; set; } = new();
        public List<ChartCategoryItem> CategoryData { get; set; } = new();
        public int TotalRequests { get; set; }
        public int CompletedRequests { get; set; }
        public int OpenRequests { get; set; }
        public int ResolutionRate { get; set; }
    }

    public class ChartStatusBreakdownItem
    {
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }
        public string Color { get; set; } = string.Empty;
        public int Pct { get; set; }
    }

    public class ChartCategoryItem
    {
        public string Category { get; set; } = string.Empty;
        public int Count { get; set; }
        public int Completed { get; set; }
        public int Pending { get; set; }
    }

    public class PendingApprovalsSummaryDto
    {
        public List<AgencyResponse> PendingRegistrations { get; set; } = new();
        public List<PendingPropertyAssignmentDto> PendingPropertyAssignments { get; set; } = new();
    }

    public class PendingPropertyAssignmentDto : AgencyPropertyAssignmentDto
    {
        public string AgencyId { get; set; } = string.Empty;
        public string AgencyName { get; set; } = string.Empty;
    }

    public class ActivityDto
    {
        public string Id { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Time { get; set; } = "Just now";
        public string Icon { get; set; } = "CheckCircle2";
        public string? Timestamp { get; set; }
    }

    public class FileUploadResponseDto
    {
        public string Url { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        public string FileSizeFormatted { get; set; } = string.Empty;
    }
}
