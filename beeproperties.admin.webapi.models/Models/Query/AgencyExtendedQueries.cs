namespace beeproperties.admin.webapi.models.Models.Query
{
    public class GetAgencyPersonnelQuery
    {
        public string AgencyId { get; set; }
        public string? Status { get; set; }
        public string? Shift { get; set; }

        public GetAgencyPersonnelQuery(string agencyId, string? status = null, string? shift = null)
        {
            AgencyId = agencyId;
            Status = status;
            Shift = shift;
        }
    }

    public class GetAgencyPropertyAssignmentsQuery
    {
        public string AgencyId { get; set; }

        public GetAgencyPropertyAssignmentsQuery(string agencyId)
        {
            AgencyId = agencyId;
        }
    }

    public class GetAgencyApprovalHistoryQuery
    {
        public string AgencyId { get; set; }

        public GetAgencyApprovalHistoryQuery(string agencyId)
        {
            AgencyId = agencyId;
        }
    }
}
