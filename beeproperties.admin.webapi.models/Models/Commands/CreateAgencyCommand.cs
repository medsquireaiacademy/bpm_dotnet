using beeproperties.admin.webapi.models.DTO.Response;

namespace beeproperties.admin.webapi.models.Models.Commands
{
    public class CreateAgencyCommand
    {
        public string? Id { get; set; }
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
        public AgencyAgentDto? Agent { get; set; }
        public AgencyCompanyDto? Company { get; set; }
        public AgencyContactsDto? Contacts { get; set; }
        public List<AgencyPersonnelDto> Personnel { get; set; } = new();
        public List<AgencyAssignmentDto> Assignments { get; set; } = new();
        public List<AgencyServiceDto> Services { get; set; } = new();
        public List<AgencyDocumentDto> Documents { get; set; } = new();
        public List<AgencyPropertyAssignmentDto> PropertyAssignments { get; set; } = new();
        public List<AgencyApprovalHistoryDto> ApprovalHistory { get; set; } = new();
    }
}

