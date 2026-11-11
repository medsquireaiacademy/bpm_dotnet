namespace beeproperties.admin.webapi.models.DTO.Response
{
    public class CommunityResponse
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
        public int TotalAmenities { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

