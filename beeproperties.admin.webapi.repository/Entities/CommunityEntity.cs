namespace beeproperties.admin.webapi.repository.Entities
{
    public class CommunityEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Tagline { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string AerialImage { get; set; } = string.Empty;
        public int TotalApartments { get; set; }
        public int TotalBlocks { get; set; }
        public int TotalFloors { get; set; }
        public int TotalFlats { get; set; }
        public string Valuation { get; set; } = string.Empty;
        public int Established { get; set; }
        public List<CommunityAmenityEntity> Amenities { get; set; } = new();
        public List<ApartmentTemplateEntity> Apartments { get; set; } = new();
        public List<CommunityFlatEntity> Flats { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

