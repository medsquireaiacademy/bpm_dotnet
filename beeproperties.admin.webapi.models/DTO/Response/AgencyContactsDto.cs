namespace beeproperties.admin.webapi.models.DTO.Response
{
    public class AgencyContactsDto
    {
        public string PrimaryContactName { get; set; } = string.Empty;
        public string PrimaryContactDesignation { get; set; } = string.Empty;
        public string PrimaryContactMobile { get; set; } = string.Empty;
        public string PrimaryContactEmail { get; set; } = string.Empty;
        public string AlternateContactPerson { get; set; } = string.Empty;
        public string AlternateContactMobile { get; set; } = string.Empty;
        public string AccountsBillingName { get; set; } = string.Empty;
        public string AccountsBillingEmail { get; set; } = string.Empty;
        public string AccountsBillingPhone { get; set; } = string.Empty;
        public string PreferredContactMethod { get; set; } = string.Empty;
        public string PreferredContactTime { get; set; } = string.Empty;
    }
}

