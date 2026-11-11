namespace beeproperties.admin.webapi.repository.Entities
{
    public class AgencyCompanyEntity
    {
        public string Name { get; set; } = string.Empty;
        public string RegisteredCompanyName { get; set; } = string.Empty;
        public string BusinessType { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Gstin { get; set; } = string.Empty;
        public string Pan { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public int YearOfEstablishment { get; set; }
        public int NumberOfEmployees { get; set; }
        public string CompanyEmail { get; set; } = string.Empty;
        public string CompanyPhone { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PinCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AuthorizedSignatoryName { get; set; } = string.Empty;
        public string AuthorizedSignatoryDesignation { get; set; } = string.Empty;
    }
}

