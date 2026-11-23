namespace beeproperties.admin.webapi.models.DTO.Response
{
    public class FloorDto
    {
        public string Id { get; set; } = string.Empty;
        public int FloorNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalFlats { get; set; }
        public int OccupiedFlats { get; set; }
        public int VacantFlats { get; set; }
        public int ActiveMaintenance { get; set; }
        public List<FlatDto> Flats { get; set; } = new();
    }

    public class FlatDto
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

    public class PropertyResidentDto
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
}
