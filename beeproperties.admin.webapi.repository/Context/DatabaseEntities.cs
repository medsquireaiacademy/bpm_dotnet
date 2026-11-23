using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace beeproperties.admin.webapi.repository.Context
{
    // ==========================================
    // 1. COMMUNITY SCHEMA
    // ==========================================
    [Table("Communities", Schema = "Community")]
    public class CommunityDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        [MaxLength(200)] public string Name { get; set; } = string.Empty;
        [MaxLength(200)] public string Location { get; set; } = string.Empty;
        public int TotalProperties { get; set; }
        public int ActiveAgencies { get; set; }
        [MaxLength(50)] public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(200)] public string UpdatedBy { get; set; } = "System";
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<CommunityAmenityDbEntity> Amenities { get; set; } = new List<CommunityAmenityDbEntity>();
    }

    [Table("Amenities", Schema = "Community")]
    public class CommunityAmenityDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int CommunityId { get; set; }
        [MaxLength(100)] public string Name { get; set; } = string.Empty;
        [MaxLength(500)] public string? Description { get; set; }
        [MaxLength(100)] public string? Icon { get; set; }
        [MaxLength(50)] public string Status { get; set; } = "Available";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(CommunityId))]
        public virtual CommunityDbEntity? Community { get; set; }
    }

    // ==========================================
    // 2. PROPERTY SCHEMA
    // ==========================================
    [Table("Properties", Schema = "Property")]
    public class PropertyDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int? CommunityId { get; set; }
        [MaxLength(50)] public string Code { get; set; } = string.Empty;
        [MaxLength(250)] public string Name { get; set; } = string.Empty;
        [MaxLength(500)] public string? Tagline { get; set; }
        [MaxLength(100)] public string PropertyType { get; set; } = "Residential Apartment";
        [MaxLength(100)] public string PropertyStatus { get; set; } = "Active - Optimal";
        [MaxLength(100)] public string? RegistrationNumber { get; set; }
        public int BuiltYear { get; set; }
        [MaxLength(100)] public string? LandArea { get; set; }
        public string? Description { get; set; }
        [MaxLength(1000)] public string? PrimaryImage { get; set; }
        public string? AdditionalImages { get; set; } // JSON array
        [MaxLength(300)] public string AddressLine1 { get; set; } = string.Empty;
        [MaxLength(300)] public string? AddressLine2 { get; set; }
        [MaxLength(100)] public string City { get; set; } = string.Empty;
        [MaxLength(100)] public string State { get; set; } = string.Empty;
        [MaxLength(20)] public string PinCode { get; set; } = string.Empty;
        [MaxLength(100)] public string Country { get; set; } = "India";
        [MaxLength(1000)] public string? MapLink { get; set; }
        [Column(TypeName = "decimal(10, 7)")] public decimal? Latitude { get; set; }
        [Column(TypeName = "decimal(10, 7)")] public decimal? Longitude { get; set; }
        [MaxLength(150)] public string? PropertyManager { get; set; }
        [MaxLength(50)] public string? PropertyManagerContact { get; set; }
        [MaxLength(50)] public string? EmergencyContact { get; set; }
        public string? InternalNotes { get; set; }
        [Column(TypeName = "decimal(3, 2)")] public decimal Rating { get; set; } = 4.8m;
        [MaxLength(50)] public string? AccentColor { get; set; }
        [MaxLength(50)] public string? BadgeColor { get; set; }
        [MaxLength(255)] public string? ArchitecturalStyle { get; set; }
        public string? Features { get; set; } // JSON array
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(200)] public string UpdatedBy { get; set; } = "System";
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<BlockDbEntity> Blocks { get; set; } = new List<BlockDbEntity>();
        public virtual ICollection<PropertyDocumentDbEntity> Documents { get; set; } = new List<PropertyDocumentDbEntity>();
        public virtual RwaAssociationDbEntity? RwaAssociation { get; set; }
    }

    [Table("Blocks", Schema = "Property")]
    public class BlockDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int PropertyId { get; set; }
        [MaxLength(50)] public string Code { get; set; } = string.Empty;
        [MaxLength(200)] public string Name { get; set; } = string.Empty;
        [MaxLength(100)] public string? Wing { get; set; }
        public int Elevators { get; set; }
        [MaxLength(100)] public string? PowerBackup { get; set; }
        [MaxLength(200)] public string? Security { get; set; }
        [MaxLength(50)] public string Status { get; set; } = "Operational";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(200)] public string UpdatedBy { get; set; } = "System";
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(PropertyId))]
        public virtual PropertyDbEntity? Property { get; set; }
        public virtual ICollection<FloorDbEntity> Floors { get; set; } = new List<FloorDbEntity>();
    }

    [Table("Floors", Schema = "Property")]
    public class FloorDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int BlockId { get; set; }
        public int FloorNumber { get; set; }
        [MaxLength(100)] public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(200)] public string UpdatedBy { get; set; } = "System";
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(BlockId))]
        public virtual BlockDbEntity? Block { get; set; }
        public virtual ICollection<FlatDbEntity> Flats { get; set; } = new List<FlatDbEntity>();
    }

    [Table("Flats", Schema = "Property")]
    public class FlatDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int FloorId { get; set; }
        [MaxLength(50)] public string FlatNumber { get; set; } = string.Empty;
        [MaxLength(50)] public string Status { get; set; } = "Vacant";
        [MaxLength(50)] public string? BhkType { get; set; }
        [Column(TypeName = "decimal(10, 2)")] public decimal? CarpetAreaSqFt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(200)] public string UpdatedBy { get; set; } = "System";
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(FloorId))]
        public virtual FloorDbEntity? Floor { get; set; }
        public virtual ICollection<ResidentDbEntity> Residents { get; set; } = new List<ResidentDbEntity>();
    }

    [Table("Residents", Schema = "Property")]
    public class ResidentDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int FlatId { get; set; }
        [MaxLength(50)] public string ResidentType { get; set; } = "Tenant";
        [MaxLength(200)] public string FullName { get; set; } = string.Empty;
        [MaxLength(50)] public string Phone { get; set; } = string.Empty;
        [MaxLength(150)] public string? Email { get; set; }
        public DateTime? LeaseStartDate { get; set; }
        public DateTime? LeaseEndDate { get; set; }
        [MaxLength(50)] public string? VehicleNumber { get; set; }
        [MaxLength(50)] public string? ParkingSlot { get; set; }
        [MaxLength(200)] public string? PetDetails { get; set; }
        [MaxLength(200)] public string? EmergencyContactName { get; set; }
        [MaxLength(50)] public string? EmergencyContactPhone { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";

        [ForeignKey(nameof(FlatId))]
        public virtual FlatDbEntity? Flat { get; set; }
    }

    [Table("Documents", Schema = "Property")]
    public class PropertyDocumentDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int PropertyId { get; set; }
        [MaxLength(250)] public string DocumentName { get; set; } = string.Empty;
        [MaxLength(100)] public string Category { get; set; } = string.Empty;
        [MaxLength(1000)] public string FileUrl { get; set; } = string.Empty;
        [MaxLength(50)] public string FileType { get; set; } = "application/pdf";
        public long FileSizeBytes { get; set; }
        [MaxLength(1000)] public string? Description { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(200)] public string UpdatedBy { get; set; } = "System";
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(PropertyId))]
        public virtual PropertyDbEntity? Property { get; set; }
    }

    [Table("RwaAssociations", Schema = "Property")]
    public class RwaAssociationDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int PropertyId { get; set; }
        [MaxLength(50)] public string Status { get; set; } = "Active";
        [MaxLength(200)] public string Name { get; set; } = string.Empty;
        [MaxLength(100)] public string? RegistrationNumber { get; set; }
        [MaxLength(50)] public string? RegistrationDate { get; set; }
        [MaxLength(200)] public string? President { get; set; }
        [MaxLength(200)] public string? Secretary { get; set; }
        [MaxLength(200)] public string? Treasurer { get; set; }
        [MaxLength(50)] public string? ContactPhone { get; set; }
        [MaxLength(150)] public string? ContactEmail { get; set; }
        [MaxLength(100)] public string? OfficeHours { get; set; }
        [MaxLength(100)] public string? Username { get; set; }
        [MaxLength(500)] public string? PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(200)] public string UpdatedBy { get; set; } = "System";

        [ForeignKey(nameof(PropertyId))]
        public virtual PropertyDbEntity? Property { get; set; }
        public virtual ICollection<RwaCommitteeMemberDbEntity> Committee { get; set; } = new List<RwaCommitteeMemberDbEntity>();
    }

    [Table("RwaCommitteeMembers", Schema = "Property")]
    public class RwaCommitteeMemberDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int RwaId { get; set; }
        [MaxLength(200)] public string Name { get; set; } = string.Empty;
        [MaxLength(100)] public string Role { get; set; } = string.Empty;
        [MaxLength(50)] public string Flat { get; set; } = string.Empty;
        [MaxLength(50)] public string Phone { get; set; } = string.Empty;
        [MaxLength(150)] public string? Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";

        [ForeignKey(nameof(RwaId))]
        public virtual RwaAssociationDbEntity? RwaAssociation { get; set; }
    }

    // ==========================================
    // 3. AGENCY SCHEMA
    // ==========================================
    [Table("Agencies", Schema = "Agency")]
    public class AgencyDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        [MaxLength(50)] public string? AgencyCode { get; set; }
        [MaxLength(250)] public string Name { get; set; } = string.Empty;
        [MaxLength(100)] public string PrimaryCategory { get; set; } = string.Empty;
        public string? Categories { get; set; } // JSON array
        [MaxLength(50)] public string? LogoText { get; set; }
        [MaxLength(1000)] public string? LogoUrl { get; set; }
        [MaxLength(150)] public string? ContactPerson { get; set; }
        [MaxLength(50)] public string? Phone { get; set; }
        [MaxLength(250)] public string? Email { get; set; }
        [MaxLength(100)] public string? AgreementStatus { get; set; }
        [MaxLength(50)] public string AvailabilityStatus { get; set; } = "Available";
        [MaxLength(100)] public string? RegistrationRefNumber { get; set; }
        [MaxLength(100)] public string RegistrationStatus { get; set; } = "Submitted";
        public DateTime? SubmissionDate { get; set; }
        public bool TermsAccepted { get; set; }
        public bool DeclarationAccepted { get; set; }
        public string? AgentDetails { get; set; } // JSON
        public string? CompanyDetails { get; set; } // JSON
        public string? ContactDetails { get; set; } // JSON
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(200)] public string UpdatedBy { get; set; } = "System";
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<AgencyPersonnelDbEntity> Personnel { get; set; } = new List<AgencyPersonnelDbEntity>();
        public virtual ICollection<AgencyServiceDbEntity> Services { get; set; } = new List<AgencyServiceDbEntity>();
        public virtual ICollection<AgencyDocumentDbEntity> Documents { get; set; } = new List<AgencyDocumentDbEntity>();
        public virtual ICollection<AgencyPropertyAssignmentDbEntity> PropertyAssignments { get; set; } = new List<AgencyPropertyAssignmentDbEntity>();
        public virtual ICollection<AgencyApprovalHistoryDbEntity> ApprovalHistory { get; set; } = new List<AgencyApprovalHistoryDbEntity>();
    }

    [Table("Personnel", Schema = "Agency")]
    public class AgencyPersonnelDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int AgencyId { get; set; }
        [MaxLength(200)] public string Name { get; set; } = string.Empty;
        [MaxLength(100)] public string Role { get; set; } = string.Empty;
        [MaxLength(50)] public string? Experience { get; set; }
        [MaxLength(50)] public string? Phone { get; set; }
        [MaxLength(150)] public string? Email { get; set; }
        [MaxLength(50)] public string Status { get; set; } = "Available";
        [MaxLength(100)] public string? CurrentShift { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(200)] public string UpdatedBy { get; set; } = "System";
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(AgencyId))]
        public virtual AgencyDbEntity? Agency { get; set; }
    }

    [Table("Services", Schema = "Agency")]
    public class AgencyServiceDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int AgencyId { get; set; }
        [MaxLength(100)] public string Category { get; set; } = string.Empty;
        [MaxLength(200)] public string? Specialization { get; set; }
        public string? Description { get; set; }
        [MaxLength(200)] public string? CoverageArea { get; set; }
        public int ExperienceYears { get; set; }
        [MaxLength(500)] public string? Certifications { get; set; }
        [MaxLength(100)] public string? Availability { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";

        [ForeignKey(nameof(AgencyId))]
        public virtual AgencyDbEntity? Agency { get; set; }
    }

    [Table("Documents", Schema = "Agency")]
    public class AgencyDocumentDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int AgencyId { get; set; }
        [MaxLength(100)] public string DocumentType { get; set; } = string.Empty;
        [MaxLength(250)] public string FileName { get; set; } = string.Empty;
        [MaxLength(500)] public string FileUrl { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiryDate { get; set; }
        [MaxLength(50)] public string VerificationStatus { get; set; } = "Verified";
        [MaxLength(500)] public string? Remarks { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(AgencyId))]
        public virtual AgencyDbEntity? Agency { get; set; }
    }

    [Table("PropertyAssignments", Schema = "Agency")]
    public class AgencyPropertyAssignmentDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int AgencyId { get; set; }
        [MaxLength(200)] public string Organization { get; set; } = "Bee Property Management";
        public int PropertyId { get; set; }
        [MaxLength(200)] public string PropertyName { get; set; } = string.Empty;
        [MaxLength(50)] public string? PropertyCode { get; set; }
        [MaxLength(100)] public string? PropertyType { get; set; }
        [MaxLength(300)] public string? PropertyAddress { get; set; }
        [MaxLength(100)] public string? City { get; set; }
        [MaxLength(100)] public string? State { get; set; }
        [MaxLength(100)] public string AssignmentType { get; set; } = "Single Property";
        public string? AssignedBlocks { get; set; } // JSON
        [MaxLength(200)] public string? AssignedFloorsZones { get; set; }
        public string? AssignedFacilitiesAreas { get; set; } // JSON
        [MaxLength(50)] public string StartDate { get; set; } = string.Empty;
        [MaxLength(50)] public string? EndDate { get; set; }
        public string? RequestedServices { get; set; } // JSON
        public string? ApprovedServices { get; set; } // JSON
        public string? ApprovedBlocks { get; set; } // JSON
        [MaxLength(200)] public string? PropertyContactPerson { get; set; }
        [MaxLength(50)] public string? PropertyContactNumber { get; set; }
        [MaxLength(200)] public string? PropertyManagerSponsor { get; set; }
        public string? SupportingDocs { get; set; } // JSON
        public string? Reason { get; set; }
        [MaxLength(50)] public string Status { get; set; } = "Pending Approval";
        [MaxLength(50)] public string? ReviewDate { get; set; }
        [MaxLength(200)] public string? Reviewer { get; set; }
        public string? Comments { get; set; }
        public string? Restrictions { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";

        [ForeignKey(nameof(AgencyId))]
        public virtual AgencyDbEntity? Agency { get; set; }
    }

    [Table("ApprovalHistory", Schema = "Agency")]
    public class AgencyApprovalHistoryDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int AgencyId { get; set; }
        [MaxLength(50)] public string Timestamp { get; set; } = string.Empty;
        [MaxLength(150)] public string Type { get; set; } = string.Empty;
        [MaxLength(50)] public string Decision { get; set; } = string.Empty;
        [MaxLength(200)] public string Reviewer { get; set; } = "Super Admin";
        public string? Comments { get; set; }
        public string? ChangesRequested { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";

        [ForeignKey(nameof(AgencyId))]
        public virtual AgencyDbEntity? Agency { get; set; }
    }

    // ==========================================
    // 4. MAINTENANCE SCHEMA
    // ==========================================
    [Table("Requests", Schema = "Maintenance")]
    public class MaintenanceRequestDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        [MaxLength(50)] public string TicketNumber { get; set; } = string.Empty;
        public int PropertyId { get; set; }
        public int? BlockId { get; set; }
        public int? FloorId { get; set; }
        public int? FlatId { get; set; }
        [MaxLength(50)] public string? FlatNumber { get; set; }
        [MaxLength(250)] public string? LocationDescription { get; set; }
        [MaxLength(250)] public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [MaxLength(100)] public string Category { get; set; } = string.Empty;
        [MaxLength(50)] public string Priority { get; set; } = "Medium";
        [MaxLength(50)] public string Status { get; set; } = "Reported";
        public int? AssignedAgencyId { get; set; }
        public DateTime ReportedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ScheduledDate { get; set; }
        [MaxLength(100)] public string? ScheduledTimeSlot { get; set; }
        public string? ResolutionNotes { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(200)] public string UpdatedBy { get; set; } = "System";
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<MaintenanceAssignedPersonnelDbEntity> AssignedPersonnel { get; set; } = new List<MaintenanceAssignedPersonnelDbEntity>();
        public virtual ICollection<MaintenanceFeedbackDbEntity> Feedback { get; set; } = new List<MaintenanceFeedbackDbEntity>();
    }

    [Table("AssignedPersonnel", Schema = "Maintenance")]
    public class MaintenanceAssignedPersonnelDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int MaintenanceRequestId { get; set; }
        [MaxLength(200)] public string PersonnelName { get; set; } = string.Empty;
        [MaxLength(50)] public string AssignedDate { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";

        [ForeignKey(nameof(MaintenanceRequestId))]
        public virtual MaintenanceRequestDbEntity? MaintenanceRequest { get; set; }
    }

    [Table("Feedback", Schema = "Maintenance")]
    public class MaintenanceFeedbackDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int MaintenanceRequestId { get; set; }
        [MaxLength(200)] public string AdminName { get; set; } = "Super Admin";
        public string Message { get; set; } = string.Empty;
        [MaxLength(50)] public string Timestamp { get; set; } = string.Empty;
        [MaxLength(50)] public string Status { get; set; } = "Sent";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";

        [ForeignKey(nameof(MaintenanceRequestId))]
        public virtual MaintenanceRequestDbEntity? MaintenanceRequest { get; set; }
    }

    // ==========================================
    // 5. SECURITYOPS SCHEMA
    // ==========================================
    [Table("Deployments", Schema = "SecurityOps")]
    public class SecurityDeploymentDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int PropertyId { get; set; }
        public int AgencyId { get; set; }
        [MaxLength(200)] public string ZoneName { get; set; } = string.Empty;
        [MaxLength(100)] public string Shift { get; set; } = string.Empty;
        public int RequiredGuards { get; set; } = 1;
        public int AllocatedGuards { get; set; } = 1;
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; }
        [MaxLength(50)] public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(200)] public string UpdatedBy { get; set; } = "System";
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<SecurityDeploymentPersonnelDbEntity> Personnel { get; set; } = new List<SecurityDeploymentPersonnelDbEntity>();
    }

    [Table("DeploymentPersonnel", Schema = "SecurityOps")]
    public class SecurityDeploymentPersonnelDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int SecurityDeploymentId { get; set; }
        [MaxLength(200)] public string GuardName { get; set; } = string.Empty;
        [MaxLength(50)] public string ShiftDate { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";

        [ForeignKey(nameof(SecurityDeploymentId))]
        public virtual SecurityDeploymentDbEntity? SecurityDeployment { get; set; }
    }

    // ==========================================
    // 6. CONFIG SCHEMA
    // ==========================================
    [Table("ServiceCategories", Schema = "Config")]
    public class ServiceCategoryDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        [MaxLength(150)] public string Name { get; set; } = string.Empty;
        [MaxLength(500)] public string? Description { get; set; }
        [MaxLength(50)] public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(200)] public string UpdatedBy { get; set; } = "System";
        public bool IsDeleted { get; set; } = false;
    }

    [Table("MasterLookups", Schema = "Config")]
    public class MasterLookupDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        [MaxLength(100)] public string LookupGroup { get; set; } = string.Empty;
        [MaxLength(200)] public string LookupValue { get; set; } = string.Empty;
        public int DisplayOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(200)] public string UpdatedBy { get; set; } = "System";
        public bool IsDeleted { get; set; } = false;
    }

    [Table("AppPreferences", Schema = "Config")]
    public class AppPreferencesDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int? AdminUserId { get; set; }
        [MaxLength(50)] public string Theme { get; set; } = "light";
        public string? PreferencesJson { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(200)] public string UpdatedBy { get; set; } = "System";
        public bool IsDeleted { get; set; } = false;
    }

    // ==========================================
    // 7. AUTH SCHEMA
    // ==========================================
    [Table("Users", Schema = "Auth")]
    public class UserDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        [MaxLength(100)] public string Username { get; set; } = string.Empty;
        [MaxLength(250)] public string Email { get; set; } = string.Empty;
        [MaxLength(500)] public string PasswordHash { get; set; } = string.Empty;
        [MaxLength(200)] public string Salt { get; set; } = string.Empty;
        [MaxLength(150)] public string FullName { get; set; } = string.Empty;
        [MaxLength(10)] public string? Initials { get; set; } = "SA";
        [MaxLength(50)] public string Role { get; set; } = "Super Admin";
        [MaxLength(50)] public string? Phone { get; set; }
        [MaxLength(100)] public string? Title { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(200)] public string UpdatedBy { get; set; } = "System";
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<UserTokenDbEntity> Tokens { get; set; } = new List<UserTokenDbEntity>();
    }

    [Table("UserTokens", Schema = "Auth")]
    public class UserTokenDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int UserId { get; set; }
        [MaxLength(500)] public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(UserId))]
        public virtual UserDbEntity? User { get; set; }
    }

    [Table("Activities", Schema = "Auth")]
    public class ActivityDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        [MaxLength(50)] public string ActivityType { get; set; } = "property";
        [MaxLength(250)] public string Title { get; set; } = string.Empty;
        [MaxLength(1000)] public string Description { get; set; } = string.Empty;
        [MaxLength(50)] public string Icon { get; set; } = "CheckCircle2";
        [MaxLength(100)] public string? EntityId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)] public string CreatedBy { get; set; } = "System";
    }
}
