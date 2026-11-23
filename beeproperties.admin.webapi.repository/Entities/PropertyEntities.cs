namespace beeproperties.admin.webapi.repository.Entities
{
    public class PropertyEntity
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
        public string MapLink { get; set; } = string.Empty;
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Image { get; set; } = string.Empty;
        public List<string> AdditionalImages { get; set; } = new();
        public int BuiltYear { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public string LandArea { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PropertyManager { get; set; } = string.Empty;
        public string PropertyManagerContact { get; set; } = string.Empty;
        public string EmergencyContact { get; set; } = string.Empty;
        public string InternalNotes { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string AccentColor { get; set; } = string.Empty;
        public string BadgeColor { get; set; } = string.Empty;
        public string ArchitecturalStyle { get; set; } = string.Empty;
        public List<string> Features { get; set; } = new();
        public int TotalBlocks { get; set; }
        public int TotalFloors { get; set; }
        public int TotalFlats { get; set; }
        public int OccupiedFlats { get; set; }
        public int VacantFlats { get; set; }
        public int ActiveMaintenance { get; set; }
        public string RwaStatus { get; set; } = "Not Registered";
        public PropertyRwaEntity? Rwa { get; set; }
        public List<PropertyBlockEntity> Blocks { get; set; } = new();
        public List<PropertyDocumentEntity> Documents { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public class PropertyBlockEntity
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
        public List<PropertyFloorEntity> Floors { get; set; } = new();
    }

    public class PropertyFloorEntity
    {
        public string Id { get; set; } = string.Empty;
        public int FloorNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalFlats { get; set; }
        public int OccupiedFlats { get; set; }
        public int VacantFlats { get; set; }
        public int ActiveMaintenance { get; set; }
        public List<PropertyFlatEntity> Flats { get; set; } = new();
    }

    public class PropertyFlatEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Status { get; set; } = "Vacant";
        public string? ResidentType { get; set; }
        public string? ResidentName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? BhkType { get; set; }
        public decimal? CarpetAreaSqFt { get; set; }
        public int ActiveMaintenance { get; set; }
    }

    public class PropertyResidentEntity
    {
        public string Id { get; set; } = string.Empty;
        public string FlatId { get; set; } = string.Empty;
        public string ResidentType { get; set; } = "Tenant";
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? LeaseStartDate { get; set; }
        public string? LeaseEndDate { get; set; }
        public string? VehicleNumber { get; set; }
        public string? ParkingSlot { get; set; }
        public string? PetDetails { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class PropertyDocumentEntity
    {
        public string Id { get; set; } = string.Empty;
        public string PropertyId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public string FileSize { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        public string Description { get; set; } = string.Empty;
        public string UploadDate { get; set; } = string.Empty;
    }

    public class PropertyRwaEntity
    {
        public string Status { get; set; } = "Active";
        public string Name { get; set; } = string.Empty;
        public string RegNumber { get; set; } = string.Empty;
        public string RegDate { get; set; } = string.Empty;
        public string President { get; set; } = string.Empty;
        public string Secretary { get; set; } = string.Empty;
        public string Treasurer { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string OfficeHours { get; set; } = string.Empty;
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public int TotalMembers { get; set; }
        public List<PropertyRwaMemberEntity> Committee { get; set; } = new();
    }

    public class PropertyRwaMemberEntity
    {
        public string Id { get; set; } = string.Empty;
        public string PropertyId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Flat { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
    }
}
