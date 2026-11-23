using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;

namespace beeproperties.admin.webapi.services.Commands.Interface
{
    public interface IPropertyCommandService
    {
        Task<PropertyDetailResponse> CreatePropertyAsync(CreatePropertyCommand command, CancellationToken cancellationToken = default);
        Task<PropertyDetailResponse?> UpdatePropertyAsync(UpdatePropertyCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeletePropertyAsync(DeletePropertyCommand command, CancellationToken cancellationToken = default);

        Task<PropertyBlockDetailDto?> CreateBlockAsync(CreateBlockCommand command, CancellationToken cancellationToken = default);
        Task<PropertyBlockDetailDto?> UpdateBlockAsync(UpdateBlockCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteBlockAsync(DeleteBlockCommand command, CancellationToken cancellationToken = default);

        Task<FloorDto?> CreateFloorAsync(CreateFloorCommand command, CancellationToken cancellationToken = default);
        Task<FloorDto?> UpdateFloorAsync(UpdateFloorCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteFloorAsync(DeleteFloorCommand command, CancellationToken cancellationToken = default);

        Task<FlatDto?> CreateFlatAsync(CreateFlatCommand command, CancellationToken cancellationToken = default);
        Task<FlatDto?> UpdateFlatResidentAsync(UpdateFlatResidentCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteFlatAsync(DeleteFlatCommand command, CancellationToken cancellationToken = default);

        Task<PropertyDocumentDto?> AddDocumentAsync(AddPropertyDocumentCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteDocumentAsync(DeletePropertyDocumentCommand command, CancellationToken cancellationToken = default);

        Task<RwaDetailResponseDto?> SaveRwaAsync(SaveRwaDetailsCommand command, CancellationToken cancellationToken = default);
        Task<bool> UpdateRwaStatusAsync(UpdateRwaStatusCommand command, CancellationToken cancellationToken = default);
        Task<RwaCommitteeMemberDto?> AddRwaCommitteeMemberAsync(AddRwaCommitteeMemberCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteRwaCommitteeMemberAsync(DeleteRwaCommitteeMemberCommand command, CancellationToken cancellationToken = default);
    }

    public interface IMaintenanceCommandService
    {
        Task<MaintenanceTicketDetailDto> CreateTicketAsync(CreateMaintenanceTicketCommand command, CancellationToken cancellationToken = default);
        Task<MaintenanceTicketDetailDto?> UpdateTicketAsync(UpdateMaintenanceTicketCommand command, CancellationToken cancellationToken = default);
        Task<bool> UpdateStatusAsync(UpdateMaintenanceStatusCommand command, CancellationToken cancellationToken = default);
        Task<MaintenanceTicketDetailDto?> AllocateAgencyAsync(AllocateAgencyToMaintenanceCommand command, CancellationToken cancellationToken = default);
        Task<MaintenanceFeedbackDto?> AddFeedbackAsync(AddMaintenanceFeedbackCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteTicketAsync(DeleteMaintenanceTicketCommand command, CancellationToken cancellationToken = default);
    }

    public interface ISecurityCommandService
    {
        Task<SecurityDeploymentDto> CreateDeploymentAsync(CreateSecurityDeploymentCommand command, CancellationToken cancellationToken = default);
        Task<bool> UpdateStatusAsync(UpdateSecurityDeploymentStatusCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteDeploymentAsync(DeleteSecurityDeploymentCommand command, CancellationToken cancellationToken = default);
    }

    public interface ISettingsCommandService
    {
        Task<ServiceCategoryDto> CreateServiceCategoryAsync(CreateServiceCategoryCommand command, CancellationToken cancellationToken = default);
        Task<ServiceCategoryDto?> UpdateServiceCategoryAsync(UpdateServiceCategoryCommand command, CancellationToken cancellationToken = default);
        Task<bool> ToggleServiceCategoryStatusAsync(ToggleServiceCategoryStatusCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteServiceCategoryAsync(DeleteServiceCategoryCommand command, CancellationToken cancellationToken = default);
        Task<bool> UpdateMasterLookupAsync(UpdateMasterLookupCommand command, CancellationToken cancellationToken = default);
        Task<AppPreferencesDto> UpdateAppPreferencesAsync(UpdateAppPreferencesCommand command, CancellationToken cancellationToken = default);
        Task<bool> ResetDataAsync(ResetDataCommand command, CancellationToken cancellationToken = default);
    }

    public interface IAuthCommandService
    {
        Task<AuthResponseDto?> LoginAsync(LoginCommand command, CancellationToken cancellationToken = default);
        Task<AdminUserDto?> UpdateProfileAsync(UpdateAdminProfileCommand command, CancellationToken cancellationToken = default);
        Task<bool> ChangePasswordAsync(ChangeAdminPasswordCommand command, CancellationToken cancellationToken = default);
        Task<ActivityDto> CreateActivityAsync(CreateActivityCommand command, CancellationToken cancellationToken = default);
    }
}
