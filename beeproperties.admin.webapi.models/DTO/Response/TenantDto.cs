namespace beeproperties.admin.webapi.models.DTO.Response
{
    public class TenantDto
    {
        public string Name { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string LeaseStart { get; set; } = string.Empty;
        public string LeaseEnd { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public int Occupants { get; set; }
    }
}

