namespace beeproperties.admin.webapi.repository.Entities
{
    public class CommunityFlatEntity
    {
        public string Id { get; set; } = string.Empty;
        public string FlatNumber { get; set; } = string.Empty;
        public string ApartmentId { get; set; } = string.Empty;
        public string ApartmentCode { get; set; } = string.Empty;
        public string ApartmentName { get; set; } = string.Empty;
        public string ApartmentAccent { get; set; } = string.Empty;
        public string BlockId { get; set; } = string.Empty;
        public string BlockCode { get; set; } = string.Empty;
        public string BlockName { get; set; } = string.Empty;
        public string BlockWing { get; set; } = string.Empty;
        public int Floor { get; set; }
        public string Bhk { get; set; } = string.Empty;
        public int Bedrooms { get; set; }
        public double Bathrooms { get; set; }
        public int Sqft { get; set; }
        public decimal Rent { get; set; }
        public decimal Deposit { get; set; }
        public string TypeTag { get; set; } = string.Empty;
        public string Facing { get; set; } = string.Empty;
        public string Balcony { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string StatusNote { get; set; } = string.Empty;
        public string? CompletionDate { get; set; }
        public string ParkingSlot { get; set; } = string.Empty;
        public string SmartLockStatus { get; set; } = string.Empty;
        public int Temperature { get; set; }
        public string PowerBackup { get; set; } = string.Empty;
        public string WaterMeterReading { get; set; } = string.Empty;
        public string ElectricityMeterReading { get; set; } = string.Empty;
        public List<string> Features { get; set; } = new();
        public TenantEntity? Tenant { get; set; }
    }
}

