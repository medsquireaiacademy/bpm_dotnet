using beeproperties.admin.webapi.models.DTO.Response;

namespace beeproperties.admin.webapi.models.Models.Commands
{
    public class UpdateCommunityCommand
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
        public List<CommunityAmenityDto>? Amenities { get; set; }
        public List<ApartmentTemplateDto>? Apartments { get; set; }
        public List<CommunityFlatDto>? Flats { get; set; }
    }
}

