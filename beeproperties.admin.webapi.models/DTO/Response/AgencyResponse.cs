namespace beeproperties.admin.webapi.models.DTO.Response
{
    public class AgencyResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public List<string> Categories { get; set; } = new();
        public string LogoText { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AgreementStatus { get; set; } = string.Empty;
        public string AvailabilityStatus { get; set; } = string.Empty;
        public int TotalPersonnel { get; set; }
        public int AllocatedPersonnel { get; set; }
        public int AvailablePersonnel { get; set; }
        public int OnLeavePersonnel { get; set; }
        public int ActiveAssignmentsCount { get; set; }
        public string RegistrationRefNumber { get; set; } = string.Empty;
        public string RegistrationStatus { get; set; } = string.Empty;
        public string SubmissionDate { get; set; } = string.Empty;
        public bool TermsAccepted { get; set; }
        public bool DeclarationAccepted { get; set; }
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int TotalServices { get; set; }
        public int TotalDocuments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

