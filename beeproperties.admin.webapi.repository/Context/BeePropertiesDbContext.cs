using Microsoft.EntityFrameworkCore;

namespace beeproperties.admin.webapi.repository.Context
{
    public class BeePropertiesDbContext : DbContext
    {
        public BeePropertiesDbContext(DbContextOptions<BeePropertiesDbContext> options)
            : base(options)
        {
        }

        // ==========================================
        // 1. Community Schema
        // ==========================================
        public DbSet<CommunityDbEntity> Communities { get; set; } = null!;
        public DbSet<CommunityAmenityDbEntity> CommunityAmenities { get; set; } = null!;

        // ==========================================
        // 2. Property Schema
        // ==========================================
        public DbSet<PropertyDbEntity> Properties { get; set; } = null!;
        public DbSet<BlockDbEntity> Blocks { get; set; } = null!;
        public DbSet<FloorDbEntity> Floors { get; set; } = null!;
        public DbSet<FlatDbEntity> Flats { get; set; } = null!;
        public DbSet<ResidentDbEntity> Residents { get; set; } = null!;
        public DbSet<PropertyDocumentDbEntity> PropertyDocuments { get; set; } = null!;
        public DbSet<RwaAssociationDbEntity> RwaAssociations { get; set; } = null!;
        public DbSet<RwaCommitteeMemberDbEntity> RwaCommitteeMembers { get; set; } = null!;

        // ==========================================
        // 3. Agency Schema
        // ==========================================
        public DbSet<AgencyDbEntity> Agencies { get; set; } = null!;
        public DbSet<AgencyPersonnelDbEntity> AgencyPersonnel { get; set; } = null!;
        public DbSet<AgencyServiceDbEntity> AgencyServices { get; set; } = null!;
        public DbSet<AgencyDocumentDbEntity> AgencyDocuments { get; set; } = null!;
        public DbSet<AgencyPropertyAssignmentDbEntity> AgencyPropertyAssignments { get; set; } = null!;
        public DbSet<AgencyApprovalHistoryDbEntity> AgencyApprovalHistories { get; set; } = null!;

        // ==========================================
        // 4. Maintenance Schema
        // ==========================================
        public DbSet<MaintenanceRequestDbEntity> MaintenanceRequests { get; set; } = null!;
        public DbSet<MaintenanceAssignedPersonnelDbEntity> MaintenanceAssignedPersonnel { get; set; } = null!;
        public DbSet<MaintenanceFeedbackDbEntity> MaintenanceFeedbacks { get; set; } = null!;

        // ==========================================
        // 5. SecurityOps Schema
        // ==========================================
        public DbSet<SecurityDeploymentDbEntity> SecurityDeployments { get; set; } = null!;
        public DbSet<SecurityDeploymentPersonnelDbEntity> SecurityDeploymentPersonnel { get; set; } = null!;

        // ==========================================
        // 6. Config Schema
        // ==========================================
        public DbSet<ServiceCategoryDbEntity> ServiceCategories { get; set; } = null!;
        public DbSet<MasterLookupDbEntity> MasterLookups { get; set; } = null!;
        public DbSet<AppPreferencesDbEntity> AppPreferences { get; set; } = null!;

        // ==========================================
        // 7. Auth Schema
        // ==========================================
        public DbSet<UserDbEntity> Users { get; set; } = null!;
        public DbSet<UserTokenDbEntity> UserTokens { get; set; } = null!;
        public DbSet<ActivityDbEntity> Activities { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Community Schema
            modelBuilder.Entity<CommunityDbEntity>(entity =>
            {
                entity.ToTable("Communities", "Community");
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Guid).IsUnique();
            });

            modelBuilder.Entity<CommunityAmenityDbEntity>(entity =>
            {
                entity.ToTable("Amenities", "Community");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Community)
                      .WithMany(c => c.Amenities)
                      .HasForeignKey(e => e.CommunityId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // 2. Property Schema
            modelBuilder.Entity<PropertyDbEntity>(entity =>
            {
                entity.ToTable("Properties", "Property");
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Guid).IsUnique();
                entity.HasIndex(e => e.Code);
            });

            modelBuilder.Entity<BlockDbEntity>(entity =>
            {
                entity.ToTable("Blocks", "Property");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Property)
                      .WithMany(p => p.Blocks)
                      .HasForeignKey(e => e.PropertyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<FloorDbEntity>(entity =>
            {
                entity.ToTable("Floors", "Property");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Block)
                      .WithMany(b => b.Floors)
                      .HasForeignKey(e => e.BlockId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<FlatDbEntity>(entity =>
            {
                entity.ToTable("Flats", "Property");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Floor)
                      .WithMany(f => f.Flats)
                      .HasForeignKey(e => e.FloorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ResidentDbEntity>(entity =>
            {
                entity.ToTable("Residents", "Property");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Flat)
                      .WithMany(fl => fl.Residents)
                      .HasForeignKey(e => e.FlatId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PropertyDocumentDbEntity>(entity =>
            {
                entity.ToTable("Documents", "Property");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Property)
                      .WithMany(p => p.Documents)
                      .HasForeignKey(e => e.PropertyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RwaAssociationDbEntity>(entity =>
            {
                entity.ToTable("RwaAssociations", "Property");
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.PropertyId).IsUnique();
                entity.HasOne(e => e.Property)
                      .WithOne(p => p.RwaAssociation)
                      .HasForeignKey<RwaAssociationDbEntity>(e => e.PropertyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RwaCommitteeMemberDbEntity>(entity =>
            {
                entity.ToTable("RwaCommitteeMembers", "Property");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.RwaAssociation)
                      .WithMany(r => r.Committee)
                      .HasForeignKey(e => e.RwaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // 3. Agency Schema
            modelBuilder.Entity<AgencyDbEntity>(entity =>
            {
                entity.ToTable("Agencies", "Agency");
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Guid).IsUnique();
            });

            modelBuilder.Entity<AgencyPersonnelDbEntity>(entity =>
            {
                entity.ToTable("Personnel", "Agency");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Agency)
                      .WithMany(a => a.Personnel)
                      .HasForeignKey(e => e.AgencyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AgencyServiceDbEntity>(entity =>
            {
                entity.ToTable("Services", "Agency");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Agency)
                      .WithMany(a => a.Services)
                      .HasForeignKey(e => e.AgencyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AgencyDocumentDbEntity>(entity =>
            {
                entity.ToTable("Documents", "Agency");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Agency)
                      .WithMany(a => a.Documents)
                      .HasForeignKey(e => e.AgencyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AgencyPropertyAssignmentDbEntity>(entity =>
            {
                entity.ToTable("PropertyAssignments", "Agency");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Agency)
                      .WithMany(a => a.PropertyAssignments)
                      .HasForeignKey(e => e.AgencyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AgencyApprovalHistoryDbEntity>(entity =>
            {
                entity.ToTable("ApprovalHistory", "Agency");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Agency)
                      .WithMany(a => a.ApprovalHistory)
                      .HasForeignKey(e => e.AgencyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // 4. Maintenance Schema
            modelBuilder.Entity<MaintenanceRequestDbEntity>(entity =>
            {
                entity.ToTable("Requests", "Maintenance");
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.TicketNumber);
            });

            modelBuilder.Entity<MaintenanceAssignedPersonnelDbEntity>(entity =>
            {
                entity.ToTable("AssignedPersonnel", "Maintenance");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.MaintenanceRequest)
                      .WithMany(m => m.AssignedPersonnel)
                      .HasForeignKey(e => e.MaintenanceRequestId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MaintenanceFeedbackDbEntity>(entity =>
            {
                entity.ToTable("Feedback", "Maintenance");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.MaintenanceRequest)
                      .WithMany(m => m.Feedback)
                      .HasForeignKey(e => e.MaintenanceRequestId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // 5. SecurityOps Schema
            modelBuilder.Entity<SecurityDeploymentDbEntity>(entity =>
            {
                entity.ToTable("Deployments", "SecurityOps");
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Guid).IsUnique();
            });

            modelBuilder.Entity<SecurityDeploymentPersonnelDbEntity>(entity =>
            {
                entity.ToTable("DeploymentPersonnel", "SecurityOps");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.SecurityDeployment)
                      .WithMany(s => s.Personnel)
                      .HasForeignKey(e => e.SecurityDeploymentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // 6. Config Schema
            modelBuilder.Entity<ServiceCategoryDbEntity>(entity =>
            {
                entity.ToTable("ServiceCategories", "Config");
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<MasterLookupDbEntity>(entity =>
            {
                entity.ToTable("MasterLookups", "Config");
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.LookupGroup, e.LookupValue }).IsUnique();
            });

            modelBuilder.Entity<AppPreferencesDbEntity>(entity =>
            {
                entity.ToTable("AppPreferences", "Config");
                entity.HasKey(e => e.Id);
            });

            // 7. Auth Schema
            modelBuilder.Entity<UserDbEntity>(entity =>
            {
                entity.ToTable("Users", "Auth");
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<UserTokenDbEntity>(entity =>
            {
                entity.ToTable("UserTokens", "Auth");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Tokens)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ActivityDbEntity>(entity =>
            {
                entity.ToTable("Activities", "Auth");
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Timestamp);
            });
        }
    }
}
