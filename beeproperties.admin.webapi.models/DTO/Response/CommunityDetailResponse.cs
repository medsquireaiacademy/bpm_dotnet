namespace beeproperties.admin.webapi.models.DTO.Response
{
    public class CommunityDetailResponse : CommunityResponse
    {
        public List<CommunityAmenityDto> Amenities { get; set; } = new();
        public List<ApartmentTemplateDto> Apartments { get; set; } = new();
        public List<CommunityFlatDto> Flats { get; set; } = new();
    }
}

