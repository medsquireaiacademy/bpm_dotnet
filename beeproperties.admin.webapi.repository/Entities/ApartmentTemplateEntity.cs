namespace beeproperties.admin.webapi.repository.Entities
{
    public class ApartmentTemplateEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Tagline { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public double Rating { get; set; }
        public int BuiltYear { get; set; }
        public string AccentColor { get; set; } = string.Empty;
        public string BadgeColor { get; set; } = string.Empty;
        public string ArchitecturalStyle { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Features { get; set; } = new();
        public List<ApartmentBlockEntity> Blocks { get; set; } = new();
    }
}

