namespace beeproperties.admin.webapi.repository.Entities
{
    public class AgencyPropertyAssignmentEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Organization { get; set; } = string.Empty;
        public string PropertyId { get; set; } = string.Empty;
        public string PropertyName { get; set; } = string.Empty;
        public string PropertyCode { get; set; } = string.Empty;
        public string PropertyType { get; set; } = string.Empty;
        public string PropertyAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string AssignmentType { get; set; } = string.Empty;
        public List<string> AssignedBlocks { get; set; } = new();
        public string AssignedFloorsZones { get; set; } = string.Empty;
        public List<string> AssignedFacilitiesAreas { get; set; } = new();
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public List<string> RequestedServices { get; set; } = new();
        public List<string> ApprovedServices { get; set; } = new();
        public List<string> ApprovedBlocks { get; set; } = new();
        public string PropertyContactPerson { get; set; } = string.Empty;
        public string PropertyContactNumber { get; set; } = string.Empty;
        public string PropertyManagerSponsor { get; set; } = string.Empty;
        public List<string> SupportingDocs { get; set; } = new();
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? ReviewDate { get; set; }
        public string Reviewer { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public string Restrictions { get; set; } = string.Empty;
    }
}

