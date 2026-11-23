using beeproperties.admin.webapi.models.DTO.Response;

namespace beeproperties.admin.webapi.models.Models.Commands
{
    public class ReviewAgencyRegistrationCommand
    {
        public string AgencyId { get; set; } = string.Empty;
        public string Decision { get; set; } = "Approve"; // Approve, Reject, Request Changes
        public string? Comments { get; set; }
        public string? ChangesRequested { get; set; }
    }

    public class CreateAgencyPersonnelCommand
    {
        public string AgencyId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? Experience { get; set; } = "5 yrs";
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; } = "Available";
        public string? CurrentShift { get; set; } = "Unassigned";
    }

    public class UpdateAgencyPersonnelCommand : CreateAgencyPersonnelCommand
    {
        public string PersonnelId { get; set; } = string.Empty;
    }

    public class DeleteAgencyPersonnelCommand
    {
        public string AgencyId { get; set; } = string.Empty;
        public string PersonnelId { get; set; } = string.Empty;
    }

    public class RequestPropertyAssignmentCommand
    {
        public string AgencyId { get; set; } = string.Empty;
        public string PropertyId { get; set; } = string.Empty;
        public string PropertyName { get; set; } = string.Empty;
        public string? PropertyCode { get; set; }
        public string? PropertyType { get; set; }
        public string? PropertyAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string Organization { get; set; } = "Bee Property / Zivion Management";
        public string AssignmentType { get; set; } = "Single Property";
        public List<string> AssignedBlocks { get; set; } = new();
        public string? AssignedFloorsZones { get; set; }
        public List<string> AssignedFacilitiesAreas { get; set; } = new();
        public string StartDate { get; set; } = string.Empty;
        public string? EndDate { get; set; }
        public List<string> RequestedServices { get; set; } = new();
        public string? PropertyContactPerson { get; set; }
        public string? PropertyContactNumber { get; set; }
        public string? PropertyManagerSponsor { get; set; } = "Property Manager";
        public List<string> SupportingDocs { get; set; } = new();
        public string? Reason { get; set; }
    }

    public class ReviewPropertyAssignmentCommand
    {
        public string AgencyId { get; set; } = string.Empty;
        public string AssignmentId { get; set; } = string.Empty;
        public string Decision { get; set; } = "Approve"; // Approve, Approve with Restrictions, Request Changes, Reject
        public List<string> ApprovedServices { get; set; } = new();
        public List<string> ApprovedBlocks { get; set; } = new();
        public string? ApprovedStartDate { get; set; }
        public string? ApprovedEndDate { get; set; }
        public string? Comments { get; set; }
        public string? RejectionReason { get; set; }
        public string? Restrictions { get; set; }
        public Dictionary<string, bool>? Checklist { get; set; }
    }

    public class RevokePropertyAssignmentCommand
    {
        public string AgencyId { get; set; } = string.Empty;
        public string AssignmentId { get; set; } = string.Empty;
    }

    public class CreateAgencyServiceCommand
    {
        public string AgencyId { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? Specialization { get; set; }
        public string? Description { get; set; }
        public string? CoverageArea { get; set; }
        public int YearsOfExperience { get; set; }
        public string? Certifications { get; set; }
        public string? Availability { get; set; }
    }

    public class DeleteAgencyServiceCommand
    {
        public string AgencyId { get; set; } = string.Empty;
        public string ServiceId { get; set; } = string.Empty;
    }

    public class AddAgencyDocumentCommand
    {
        public string AgencyId { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string? FileType { get; set; } = "application/pdf";
        public long FileSizeBytes { get; set; }
        public string? ExpiryDate { get; set; }
        public string? Remarks { get; set; }
        public string? Description { get => Remarks; set => Remarks = value; }
        public string? VerificationStatus { get; set; } = "Pending Verification";
    }

    public class DeleteAgencyDocumentCommand
    {
        public string AgencyId { get; set; } = string.Empty;
        public string DocumentId { get; set; } = string.Empty;
    }
}
