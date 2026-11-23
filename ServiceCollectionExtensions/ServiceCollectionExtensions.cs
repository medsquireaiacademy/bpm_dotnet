using beeproperties.admin.webapi.Handler.Command;
using beeproperties.admin.webapi.Handler.Query;
using beeproperties.admin.webapi.repository.Commands;
using beeproperties.admin.webapi.repository.Commands.Interface;
using beeproperties.admin.webapi.repository.Context;
using beeproperties.admin.webapi.repository.Data;
using beeproperties.admin.webapi.repository.Query;
using beeproperties.admin.webapi.repository.Query.Interface;
using beeproperties.admin.webapi.services.Commands;
using beeproperties.admin.webapi.services.Commands.Interface;
using beeproperties.admin.webapi.services.Query;
using beeproperties.admin.webapi.services.Query.Interface;
using Microsoft.EntityFrameworkCore;

namespace beeproperties.admin.webapi.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration? configuration = null)
        {
            // Entity Framework Core DbContext
            services.AddDbContext<BeePropertiesDbContext>((serviceProvider, options) =>
            {
                var config = configuration ?? serviceProvider.GetRequiredService<IConfiguration>();
                var connectionString = config.GetConnectionString("DefaultConnection")
                    ?? "Server=13.204.39.91;Database=YodaLIMS;User Id=sa;Password=dev1@dms;TrustServerCertificate=True;MultipleActiveResultSets=true;";
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();

            // Data Stores (In-memory singletons seeded from complete_application_data.json)
            services.AddSingleton<ICommunityDataStore, CommunityDataStore>();
            services.AddSingleton<IAgencyDataStore, AgencyDataStore>();
            services.AddSingleton<IPropertyDataStore, PropertyDataStore>();
            services.AddSingleton<IMaintenanceDataStore, MaintenanceDataStore>();
            services.AddSingleton<ISecurityDataStore, SecurityDataStore>();
            services.AddSingleton<ISettingsDataStore, SettingsDataStore>();
            services.AddSingleton<IAuthDataStore, AuthDataStore>();

            // Repositories
            services.AddScoped<ICommunityCommandRepository, CommunityCommandRepository>()
                    .AddScoped<ICommunityQueryRepository, CommunityQueryRepository>();
            services.AddScoped<IAgencyCommandRepository, AgencyCommandRepository>()
                    .AddScoped<IAgencyQueryRepository, AgencyQueryRepository>();
            services.AddScoped<IPropertyCommandRepository, PropertyCommandRepository>()
                    .AddScoped<IPropertyQueryRepository, PropertyQueryRepository>();
            services.AddScoped<IMaintenanceCommandRepository, MaintenanceCommandRepository>()
                    .AddScoped<IMaintenanceQueryRepository, MaintenanceQueryRepository>();
            services.AddScoped<ISecurityCommandRepository, SecurityCommandRepository>()
                    .AddScoped<ISecurityQueryRepository, SecurityQueryRepository>();
            services.AddScoped<ISettingsCommandRepository, SettingsCommandRepository>()
                    .AddScoped<ISettingsQueryRepository, SettingsQueryRepository>();
            services.AddScoped<IAuthCommandRepository, AuthCommandRepository>()
                    .AddScoped<IAuthQueryRepository, AuthQueryRepository>();

            // Services
            services.AddScoped<ICommunityCommandService, CommunityCommandService>()
                    .AddScoped<ICommunityQueryService, CommunityQueryService>();
            services.AddScoped<IAgencyCommandService, AgencyCommandService>()
                    .AddScoped<IAgencyQueryService, AgencyQueryService>();
            services.AddScoped<IPropertyCommandService, PropertyCommandService>()
                    .AddScoped<IPropertyQueryService, PropertyQueryService>();
            services.AddScoped<IMaintenanceCommandService, MaintenanceCommandService>()
                    .AddScoped<IMaintenanceQueryService, MaintenanceQueryService>();
            services.AddScoped<ISecurityCommandService, SecurityCommandService>()
                    .AddScoped<ISecurityQueryService, SecurityQueryService>();
            services.AddScoped<ISettingsCommandService, SettingsCommandService>()
                    .AddScoped<ISettingsQueryService, SettingsQueryService>();
            services.AddScoped<IAuthCommandService, AuthCommandService>()
                    .AddScoped<IAuthQueryService, AuthQueryService>();
            services.AddScoped<IDashboardQueryService, DashboardQueryService>();

            // Community Handlers
            services.AddScoped<ICreateCommunityHandler, CreateCommunityHandler>()
                    .AddScoped<IUpdateCommunityHandler, UpdateCommunityHandler>()
                    .AddScoped<IDeleteCommunityHandler, DeleteCommunityHandler>()
                    .AddScoped<IGetAllCommunitiesHandler, GetAllCommunitiesHandler>()
                    .AddScoped<IGetCommunityByIdHandler, GetCommunityByIdHandler>();

            // Agency Handlers
            services.AddScoped<ICreateAgencyHandler, CreateAgencyHandler>()
                    .AddScoped<IUpdateAgencyHandler, UpdateAgencyHandler>()
                    .AddScoped<IDeleteAgencyHandler, DeleteAgencyHandler>()
                    .AddScoped<IGetAllAgenciesHandler, GetAllAgenciesHandler>()
                    .AddScoped<IGetAgencyByIdHandler, GetAgencyByIdHandler>()
                    .AddScoped<IReviewAgencyRegistrationHandler, ReviewAgencyRegistrationHandler>()
                    .AddScoped<ICreateAgencyPersonnelHandler, CreateAgencyPersonnelHandler>()
                    .AddScoped<IUpdateAgencyPersonnelHandler, UpdateAgencyPersonnelHandler>()
                    .AddScoped<IDeleteAgencyPersonnelHandler, DeleteAgencyPersonnelHandler>()
                    .AddScoped<IRequestPropertyAssignmentHandler, RequestPropertyAssignmentHandler>()
                    .AddScoped<IReviewPropertyAssignmentHandler, ReviewPropertyAssignmentHandler>()
                    .AddScoped<IRevokePropertyAssignmentHandler, RevokePropertyAssignmentHandler>()
                    .AddScoped<ICreateAgencyServiceHandler, CreateAgencyServiceHandler>()
                    .AddScoped<IDeleteAgencyServiceHandler, DeleteAgencyServiceHandler>()
                    .AddScoped<IAddAgencyDocumentHandler, AddAgencyDocumentHandler>()
                    .AddScoped<IDeleteAgencyDocumentHandler, DeleteAgencyDocumentHandler>()
                    .AddScoped<IGetAgencyPersonnelHandler, GetAgencyPersonnelHandler>()
                    .AddScoped<IGetAgencyPropertyAssignmentsHandler, GetAgencyPropertyAssignmentsHandler>()
                    .AddScoped<IGetAgencyApprovalHistoryHandler, GetAgencyApprovalHistoryHandler>();

            // Property Handlers
            services.AddScoped<ICreatePropertyHandler, CreatePropertyHandler>()
                    .AddScoped<IUpdatePropertyHandler, UpdatePropertyHandler>()
                    .AddScoped<IDeletePropertyHandler, DeletePropertyHandler>()
                    .AddScoped<ICreateBlockHandler, CreateBlockHandler>()
                    .AddScoped<IUpdateBlockHandler, UpdateBlockHandler>()
                    .AddScoped<IDeleteBlockHandler, DeleteBlockHandler>()
                    .AddScoped<ICreateFloorHandler, CreateFloorHandler>()
                    .AddScoped<IUpdateFloorHandler, UpdateFloorHandler>()
                    .AddScoped<IDeleteFloorHandler, DeleteFloorHandler>()
                    .AddScoped<ICreateFlatHandler, CreateFlatHandler>()
                    .AddScoped<IUpdateFlatResidentHandler, UpdateFlatResidentHandler>()
                    .AddScoped<IDeleteFlatHandler, DeleteFlatHandler>()
                    .AddScoped<IAddPropertyDocumentHandler, AddPropertyDocumentHandler>()
                    .AddScoped<IDeletePropertyDocumentHandler, DeletePropertyDocumentHandler>()
                    .AddScoped<ISaveRwaHandler, SaveRwaHandler>()
                    .AddScoped<IUpdateRwaStatusHandler, UpdateRwaStatusHandler>()
                    .AddScoped<IAddRwaCommitteeMemberHandler, AddRwaCommitteeMemberHandler>()
                    .AddScoped<IDeleteRwaCommitteeMemberHandler, DeleteRwaCommitteeMemberHandler>()
                    .AddScoped<IGetAllPropertiesHandler, GetAllPropertiesHandler>()
                    .AddScoped<IGetPropertyByIdHandler, GetPropertyByIdHandler>()
                    .AddScoped<IGetPropertySummaryHandler, GetPropertySummaryHandler>()
                    .AddScoped<IGetPropertyBlocksHandler, GetPropertyBlocksHandler>()
                    .AddScoped<IGetBlockFloorsHandler, GetBlockFloorsHandler>()
                    .AddScoped<IGetFloorFlatsHandler, GetFloorFlatsHandler>()
                    .AddScoped<IGetFlatByNumberHandler, GetFlatByNumberHandler>()
                    .AddScoped<IGetPropertyDocumentsHandler, GetPropertyDocumentsHandler>()
                    .AddScoped<IGetPropertyRwaHandler, GetPropertyRwaHandler>();

            // Maintenance Handlers
            services.AddScoped<ICreateMaintenanceTicketHandler, CreateMaintenanceTicketHandler>()
                    .AddScoped<IUpdateMaintenanceTicketHandler, UpdateMaintenanceTicketHandler>()
                    .AddScoped<IUpdateMaintenanceStatusHandler, UpdateMaintenanceStatusHandler>()
                    .AddScoped<IAllocateAgencyToMaintenanceHandler, AllocateAgencyToMaintenanceHandler>()
                    .AddScoped<IAddMaintenanceFeedbackHandler, AddMaintenanceFeedbackHandler>()
                    .AddScoped<IDeleteMaintenanceTicketHandler, DeleteMaintenanceTicketHandler>()
                    .AddScoped<IGetAllMaintenanceRequestsHandler, GetAllMaintenanceRequestsHandler>()
                    .AddScoped<IGetMaintenanceRequestByIdHandler, GetMaintenanceRequestByIdHandler>()
                    .AddScoped<IGetCompatibleAgenciesHandler, GetCompatibleAgenciesHandler>();

            // Security Handlers
            services.AddScoped<ICreateSecurityDeploymentHandler, CreateSecurityDeploymentHandler>()
                    .AddScoped<IUpdateSecurityDeploymentStatusHandler, UpdateSecurityDeploymentStatusHandler>()
                    .AddScoped<IDeleteSecurityDeploymentHandler, DeleteSecurityDeploymentHandler>()
                    .AddScoped<IGetAllSecurityDeploymentsHandler, GetAllSecurityDeploymentsHandler>()
                    .AddScoped<IGetSecurityDeploymentByIdHandler, GetSecurityDeploymentByIdHandler>();

            // Settings Handlers
            services.AddScoped<ICreateServiceCategoryHandler, CreateServiceCategoryHandler>()
                    .AddScoped<IUpdateServiceCategoryHandler, UpdateServiceCategoryHandler>()
                    .AddScoped<IToggleServiceCategoryStatusHandler, ToggleServiceCategoryStatusHandler>()
                    .AddScoped<IDeleteServiceCategoryHandler, DeleteServiceCategoryHandler>()
                    .AddScoped<IUpdateMasterLookupHandler, UpdateMasterLookupHandler>()
                    .AddScoped<IUpdateAppPreferencesHandler, UpdateAppPreferencesHandler>()
                    .AddScoped<IResetDataHandler, ResetDataHandler>()
                    .AddScoped<IGetAllServiceCategoriesHandler, GetAllServiceCategoriesHandler>()
                    .AddScoped<IGetMasterLookupsHandler, GetMasterLookupsHandler>()
                    .AddScoped<IGetAppPreferencesHandler, GetAppPreferencesHandler>();

            // Auth and Dashboard Handlers
            services.AddScoped<ILoginHandler, LoginHandler>()
                    .AddScoped<IUpdateAdminProfileHandler, UpdateAdminProfileHandler>()
                    .AddScoped<IChangeAdminPasswordHandler, ChangeAdminPasswordHandler>()
                    .AddScoped<ICreateActivityHandler, CreateActivityHandler>()
                    .AddScoped<IGetCurrentUserHandler, GetCurrentUserHandler>()
                    .AddScoped<IGetRecentActivitiesHandler, GetRecentActivitiesHandler>()
                    .AddScoped<IGetDashboardMetricsHandler, GetDashboardMetricsHandler>()
                    .AddScoped<IGetServiceRequestChartsHandler, GetServiceRequestChartsHandler>()
                    .AddScoped<IGetPendingApprovalsHandler, GetPendingApprovalsHandler>();

            return services;
        }
    }
}
