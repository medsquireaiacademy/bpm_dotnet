using beeproperties.admin.webapi.models.DTO.Response;

namespace beeproperties.admin.webapi.models.Models.Commands
{
    public class CreatePropertyCommand
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Tagline { get; set; }
        public string PropertyType { get; set; } = "Residential Apartment";
        public string Status { get; set; } = "Active - Optimal";
        public string Address { get; set; } = string.Empty;
        public string? Address2 { get; set; }
        public string City { get; set; } = "Hyderabad";
        public string State { get; set; } = "Telangana";
        public string Zip { get; set; } = "500032";
        public string Country { get; set; } = "India";
        public string? MapLink { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? Image { get; set; }
        public List<string> AdditionalImages { get; set; } = new();
        public int BuiltYear { get; set; } = DateTime.UtcNow.Year;
        public string? RegistrationNumber { get; set; }
        public string? LandArea { get; set; }
        public string? Description { get; set; }
        public string? PropertyManager { get; set; }
        public string? PropertyManagerContact { get; set; }
        public string? EmergencyContact { get; set; }
        public string? InternalNotes { get; set; }
        public string? RwaStatus { get; set; }
        public List<PropertyBlockDetailDto> Blocks { get; set; } = new();
        public List<PropertyDocumentDto> Documents { get; set; } = new();
    }

    public class UpdatePropertyCommand : CreatePropertyCommand
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class DeletePropertyCommand
    {
        public string Id { get; set; }

        public DeletePropertyCommand(string id)
        {
            Id = id;
        }
    }

    public class CreateBlockCommand
    {
        public string PropertyId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Wing { get; set; }
        public int FloorsCount { get; set; } = 4;
        public int FlatsPerFloor { get; set; } = 4;
        public int Elevators { get; set; } = 2;
        public string? PowerBackup { get; set; } = "100% DG Backed";
        public string? Security { get; set; } = "Biometric FaceID + RFID";
        public string? Status { get; set; } = "Operational";
    }

    public class UpdateBlockCommand
    {
        public string PropertyId { get; set; } = string.Empty;
        public string BlockId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Wing { get; set; }
        public int Elevators { get; set; }
        public string? PowerBackup { get; set; }
        public string? Security { get; set; }
        public string? Status { get; set; }
    }

    public class DeleteBlockCommand
    {
        public string PropertyId { get; set; } = string.Empty;
        public string BlockId { get; set; } = string.Empty;
    }

    public class CreateFloorCommand
    {
        public string PropertyId { get; set; } = string.Empty;
        public string BlockId { get; set; } = string.Empty;
        public int? FloorNumber { get; set; }
        public string? Name { get; set; }
        public int FlatsCount { get; set; } = 4;
    }

    public class UpdateFloorCommand
    {
        public string PropertyId { get; set; } = string.Empty;
        public string BlockId { get; set; } = string.Empty;
        public string FloorId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class DeleteFloorCommand
    {
        public string PropertyId { get; set; } = string.Empty;
        public string BlockId { get; set; } = string.Empty;
        public string FloorId { get; set; } = string.Empty;
    }

    public class CreateFlatCommand
    {
        public string PropertyId { get; set; } = string.Empty;
        public string BlockId { get; set; } = string.Empty;
        public string FloorId { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Status { get; set; } = "Vacant";
        public string? BhkType { get; set; }
        public decimal? CarpetAreaSqFt { get; set; }
        public string? ResidentType { get; set; }
        public string? ResidentName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }

    public class UpdateFlatResidentCommand
    {
        public string PropertyId { get; set; } = string.Empty;
        public string BlockId { get; set; } = string.Empty;
        public string FloorId { get; set; } = string.Empty;
        public string FlatNumber { get; set; } = string.Empty;
        public string Status { get; set; } = "Vacant";
        public string? ResidentType { get; set; }
        public string? ResidentName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }

    public class DeleteFlatCommand
    {
        public string PropertyId { get; set; } = string.Empty;
        public string BlockId { get; set; } = string.Empty;
        public string FloorId { get; set; } = string.Empty;
        public string FlatNumber { get; set; } = string.Empty;
    }

    public class AddPropertyDocumentCommand
    {
        public string PropertyId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string FileType { get; set; } = "application/pdf";
        public string FileSize { get; set; } = "2.4 MB";
        public long FileSizeBytes { get; set; }
        public string? Description { get; set; }
    }

    public class DeletePropertyDocumentCommand
    {
        public string PropertyId { get; set; } = string.Empty;
        public string DocumentId { get; set; } = string.Empty;
    }

    public class SaveRwaDetailsCommand
    {
        public string PropertyId { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
        public string Name { get; set; } = string.Empty;
        public string? RegNumber { get; set; }
        public string? RegDate { get; set; }
        public string? President { get; set; }
        public string? Secretary { get; set; }
        public string? Treasurer { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public string? OfficeHours { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public List<RwaCommitteeMemberDto> Committee { get; set; } = new();
    }

    public class UpdateRwaStatusCommand
    {
        public string PropertyId { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
    }

    public class AddRwaCommitteeMemberCommand
    {
        public string PropertyId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Flat { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
    }

    public class DeleteRwaCommitteeMemberCommand
    {
        public string PropertyId { get; set; } = string.Empty;
        public string MemberId { get; set; } = string.Empty;
    }
}
