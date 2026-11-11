namespace beeproperties.admin.webapi.repository.Entities
{
    public class AgencyEntity
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

        public AgencyAgentEntity? Agent { get; set; }
        public AgencyCompanyEntity? Company { get; set; }
        public AgencyContactsEntity? Contacts { get; set; }

        public List<AgencyPersonnelEntity> Personnel { get; set; } = new();
        public List<AgencyAssignmentEntity> Assignments { get; set; } = new();
        public List<AgencyServiceEntity> Services { get; set; } = new();
        public List<AgencyDocumentEntity> Documents { get; set; } = new();
        public List<AgencyPropertyAssignmentEntity> PropertyAssignments { get; set; } = new();
        public List<AgencyApprovalHistoryEntity> ApprovalHistory { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

