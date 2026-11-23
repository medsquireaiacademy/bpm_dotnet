namespace beeproperties.admin.webapi.repository.Entities
{
    public class SecurityDeploymentEntity
    {
        public string Id { get; set; } = string.Empty;
        public string ApartmentId { get; set; } = string.Empty;
        public string PropertyId { get => ApartmentId; set => ApartmentId = value; }
        public string ApartmentName { get; set; } = string.Empty;
        public string PropertyName { get => ApartmentName; set => ApartmentName = value; }
        public string AgencyId { get; set; } = string.Empty;
        public string AgencyName { get; set; } = string.Empty;
        public string BlockName { get; set; } = string.Empty;
        public string Shift { get; set; } = string.Empty;
        public int RequiredGuards { get; set; }
        public int AllocatedGuards { get; set; }
        public List<string> PersonnelNames { get; set; } = new();
        public string StartDate { get; set; } = string.Empty;
        public string? EndDate { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public class SecurityDeploymentPersonnelEntity
    {
        public string Id { get; set; } = string.Empty;
        public string SecurityDeploymentId { get; set; } = string.Empty;
        public string GuardName { get; set; } = string.Empty;
        public string ShiftDate { get; set; } = string.Empty;
    }
}
