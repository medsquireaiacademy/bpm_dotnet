namespace beeproperties.admin.webapi.repository.Entities
{
    public class ApartmentBlockEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Wing { get; set; } = string.Empty;
        public int Elevators { get; set; }
        public string PowerBackup { get; set; } = string.Empty;
        public string Security { get; set; } = string.Empty;
    }
}

