namespace beeproperties.admin.webapi.models.DTO.Response
{
    public class AgencyDetailResponse : AgencyResponse
    {
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

