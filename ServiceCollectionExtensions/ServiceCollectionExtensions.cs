using beeproperties.admin.webapi.Handler.Command;
using beeproperties.admin.webapi.Handler.Query;
using beeproperties.admin.webapi.repository.Commands;
using beeproperties.admin.webapi.repository.Commands.Interface;
using beeproperties.admin.webapi.repository.Data;
using beeproperties.admin.webapi.repository.Query;
using beeproperties.admin.webapi.repository.Query.Interface;
using beeproperties.admin.webapi.services.Commands;
using beeproperties.admin.webapi.services.Commands.Interface;
using beeproperties.admin.webapi.services.Query;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            // Data Store
            services.AddSingleton<ICommunityDataStore, CommunityDataStore>();
            services.AddSingleton<IAgencyDataStore, AgencyDataStore>();

            // Repositories
            services.AddScoped<ICommunityCommandRepository, CommunityCommandRepository>()
                    .AddScoped<ICommunityQueryRepository, CommunityQueryRepository>();
            services.AddScoped<IAgencyCommandRepository, AgencyCommandRepository>()
                    .AddScoped<IAgencyQueryRepository, AgencyQueryRepository>();

            // Services
            services.AddScoped<ICommunityCommandService, CommunityCommandService>()
                    .AddScoped<ICommunityQueryService, CommunityQueryService>();
            services.AddScoped<IAgencyCommandService, AgencyCommandService>()
                    .AddScoped<IAgencyQueryService, AgencyQueryService>();

            // Handlers
            services.AddScoped<ICreateCommunityHandler, CreateCommunityHandler>()
                    .AddScoped<IUpdateCommunityHandler, UpdateCommunityHandler>()
                    .AddScoped<IDeleteCommunityHandler, DeleteCommunityHandler>()
                    .AddScoped<IGetAllCommunitiesHandler, GetAllCommunitiesHandler>()
                    .AddScoped<IGetCommunityByIdHandler, GetCommunityByIdHandler>();

            services.AddScoped<ICreateAgencyHandler, CreateAgencyHandler>()
                    .AddScoped<IUpdateAgencyHandler, UpdateAgencyHandler>()
                    .AddScoped<IDeleteAgencyHandler, DeleteAgencyHandler>()
                    .AddScoped<IGetAllAgenciesHandler, GetAllAgenciesHandler>()
                    .AddScoped<IGetAgencyByIdHandler, GetAgencyByIdHandler>();

            return services;
        }
    }
}

