using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.Handler.Query
{
    public interface IGetAllServiceCategoriesHandler
    {
        Task<List<ServiceCategoryDto>> HandleAsync(GetAllServiceCategoriesQuery query, CancellationToken cancellationToken = default);
    }

    public class GetAllServiceCategoriesHandler : IGetAllServiceCategoriesHandler
    {
        private readonly ISettingsQueryService _service;
        public GetAllServiceCategoriesHandler(ISettingsQueryService service) => _service = service;
        public Task<List<ServiceCategoryDto>> HandleAsync(GetAllServiceCategoriesQuery query, CancellationToken cancellationToken = default) =>
            _service.GetAllServiceCategoriesAsync(query, cancellationToken);
    }

    public interface IGetMasterLookupsHandler
    {
        Task<MasterLookupsDto> HandleAsync(GetMasterLookupsQuery query, CancellationToken cancellationToken = default);
    }

    public class GetMasterLookupsHandler : IGetMasterLookupsHandler
    {
        private readonly ISettingsQueryService _service;
        public GetMasterLookupsHandler(ISettingsQueryService service) => _service = service;
        public Task<MasterLookupsDto> HandleAsync(GetMasterLookupsQuery query, CancellationToken cancellationToken = default) =>
            _service.GetMasterLookupsAsync(query, cancellationToken);
    }

    public interface IGetAppPreferencesHandler
    {
        Task<AppPreferencesDto> HandleAsync(GetAppPreferencesQuery query, CancellationToken cancellationToken = default);
    }

    public class GetAppPreferencesHandler : IGetAppPreferencesHandler
    {
        private readonly ISettingsQueryService _service;
        public GetAppPreferencesHandler(ISettingsQueryService service) => _service = service;
        public Task<AppPreferencesDto> HandleAsync(GetAppPreferencesQuery query, CancellationToken cancellationToken = default) =>
            _service.GetAppPreferencesAsync(query, cancellationToken);
    }
}
