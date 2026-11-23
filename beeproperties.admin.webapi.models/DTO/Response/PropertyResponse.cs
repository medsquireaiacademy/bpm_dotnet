namespace beeproperties.admin.webapi.models.DTO.Response
{
    public class PropertyResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Tagline { get; set; } = string.Empty;
        public string PropertyType { get; set; } = "Residential Apartment";
        public string Status { get; set; } = "Active - Optimal";
        public string Address { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string Country { get; set; } = "India";
        public string Image { get; set; } = string.Empty;
        public int BuiltYear { get; set; }
        public int TotalBlocks { get; set; }
        public int TotalFloors { get; set; }
        public int TotalFlats { get; set; }
        public int OccupiedFlats { get; set; }
        public int VacantFlats { get; set; }
        public int ActiveMaintenance { get; set; }
        public string RwaStatus { get; set; } = "Not Registered";
        public double OccupancyPercentage => TotalFlats > 0 ? Math.Round(((double)OccupiedFlats / TotalFlats) * 100, 1) : 0;
    }

    public class PropertyDetailResponse : PropertyResponse
    {
        public string LandArea { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string PropertyManager { get; set; } = string.Empty;
        public string PropertyManagerContact { get; set; } = string.Empty;
        public string EmergencyContact { get; set; } = string.Empty;
        public string InternalNotes { get; set; } = string.Empty;
        public string MapLink { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string AccentColor { get; set; } = string.Empty;
        public string BadgeColor { get; set; } = string.Empty;
        public string ArchitecturalStyle { get; set; } = string.Empty;
        public List<string> AdditionalImages { get; set; } = new();
        public List<string> Features { get; set; } = new();
        public List<PropertyBlockDetailDto> Blocks { get; set; } = new();
        public List<PropertyDocumentDto> Documents { get; set; } = new();
        public RwaDetailResponseDto? Rwa { get; set; }
    }

    public class PropertyBlockDetailDto
    {
        public string Id { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Wing { get; set; } = string.Empty;
        public int Elevators { get; set; }
        public string PowerBackup { get; set; } = string.Empty;
        public string Security { get; set; } = string.Empty;
        public int FloorsCount { get; set; }
        public int FlatsCount { get; set; }
        public int OccupiedFlats { get; set; }
        public int VacantFlats { get; set; }
        public int ActiveMaintenance { get; set; }
        public string Status { get; set; } = "Operational";
        public List<string> AssignedAgencies { get; set; } = new();
        public List<FloorDto> Floors { get; set; } = new();
    }

    public class PropertySummaryDto
    {
        public int TotalProperties { get; set; }
        public int TotalBlocks { get; set; }
        public int TotalFloors { get; set; }
        public int TotalFlats { get; set; }
        public int OccupiedFlats { get; set; }
        public int VacantFlats { get; set; }
        public double OccupancyRate { get; set; }
        public int ActiveMaintenanceRequests { get; set; }
    }
}
