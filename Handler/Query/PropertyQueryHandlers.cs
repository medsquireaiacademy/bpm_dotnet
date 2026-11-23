using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.Handler.Query
{
    public interface IGetAllPropertiesHandler
    {
        Task<List<PropertyResponse>> HandleAsync(GetAllPropertiesQuery query, CancellationToken cancellationToken = default);
    }

    public class GetAllPropertiesHandler : IGetAllPropertiesHandler
    {
        private readonly IPropertyQueryService _service;
        public GetAllPropertiesHandler(IPropertyQueryService service) => _service = service;
        public Task<List<PropertyResponse>> HandleAsync(GetAllPropertiesQuery query, CancellationToken cancellationToken = default) =>
            _service.GetAllPropertiesAsync(query, cancellationToken);
    }

    public interface IGetPropertyByIdHandler
    {
        Task<PropertyDetailResponse?> HandleAsync(GetPropertyByIdQuery query, CancellationToken cancellationToken = default);
    }

    public class GetPropertyByIdHandler : IGetPropertyByIdHandler
    {
        private readonly IPropertyQueryService _service;
        public GetPropertyByIdHandler(IPropertyQueryService service) => _service = service;
        public Task<PropertyDetailResponse?> HandleAsync(GetPropertyByIdQuery query, CancellationToken cancellationToken = default) =>
            _service.GetPropertyByIdAsync(query, cancellationToken);
    }

    public interface IGetPropertySummaryHandler
    {
        Task<PropertySummaryDto> HandleAsync(GetPropertySummaryQuery query, CancellationToken cancellationToken = default);
    }

    public class GetPropertySummaryHandler : IGetPropertySummaryHandler
    {
        private readonly IPropertyQueryService _service;
        public GetPropertySummaryHandler(IPropertyQueryService service) => _service = service;
        public Task<PropertySummaryDto> HandleAsync(GetPropertySummaryQuery query, CancellationToken cancellationToken = default) =>
            _service.GetPropertySummaryAsync(query, cancellationToken);
    }

    public interface IGetPropertyBlocksHandler
    {
        Task<List<PropertyBlockDetailDto>> HandleAsync(GetPropertyBlocksQuery query, CancellationToken cancellationToken = default);
    }

    public class GetPropertyBlocksHandler : IGetPropertyBlocksHandler
    {
        private readonly IPropertyQueryService _service;
        public GetPropertyBlocksHandler(IPropertyQueryService service) => _service = service;
        public Task<List<PropertyBlockDetailDto>> HandleAsync(GetPropertyBlocksQuery query, CancellationToken cancellationToken = default) =>
            _service.GetPropertyBlocksAsync(query, cancellationToken);
    }

    public interface IGetBlockFloorsHandler
    {
        Task<List<FloorDto>> HandleAsync(GetBlockFloorsQuery query, CancellationToken cancellationToken = default);
    }

    public class GetBlockFloorsHandler : IGetBlockFloorsHandler
    {
        private readonly IPropertyQueryService _service;
        public GetBlockFloorsHandler(IPropertyQueryService service) => _service = service;
        public Task<List<FloorDto>> HandleAsync(GetBlockFloorsQuery query, CancellationToken cancellationToken = default) =>
            _service.GetBlockFloorsAsync(query, cancellationToken);
    }

    public interface IGetFloorFlatsHandler
    {
        Task<List<FlatDto>> HandleAsync(GetFloorFlatsQuery query, CancellationToken cancellationToken = default);
    }

    public class GetFloorFlatsHandler : IGetFloorFlatsHandler
    {
        private readonly IPropertyQueryService _service;
        public GetFloorFlatsHandler(IPropertyQueryService service) => _service = service;
        public Task<List<FlatDto>> HandleAsync(GetFloorFlatsQuery query, CancellationToken cancellationToken = default) =>
            _service.GetFloorFlatsAsync(query, cancellationToken);
    }

    public interface IGetFlatByNumberHandler
    {
        Task<FlatDto?> HandleAsync(GetFlatByNumberQuery query, CancellationToken cancellationToken = default);
    }

    public class GetFlatByNumberHandler : IGetFlatByNumberHandler
    {
        private readonly IPropertyQueryService _service;
        public GetFlatByNumberHandler(IPropertyQueryService service) => _service = service;
        public Task<FlatDto?> HandleAsync(GetFlatByNumberQuery query, CancellationToken cancellationToken = default) =>
            _service.GetFlatByNumberAsync(query, cancellationToken);
    }

    public interface IGetPropertyDocumentsHandler
    {
        Task<List<PropertyDocumentDto>> HandleAsync(GetPropertyDocumentsQuery query, CancellationToken cancellationToken = default);
    }

    public class GetPropertyDocumentsHandler : IGetPropertyDocumentsHandler
    {
        private readonly IPropertyQueryService _service;
        public GetPropertyDocumentsHandler(IPropertyQueryService service) => _service = service;
        public Task<List<PropertyDocumentDto>> HandleAsync(GetPropertyDocumentsQuery query, CancellationToken cancellationToken = default) =>
            _service.GetPropertyDocumentsAsync(query, cancellationToken);
    }

    public interface IGetPropertyRwaHandler
    {
        Task<RwaDetailResponseDto?> HandleAsync(GetPropertyRwaQuery query, CancellationToken cancellationToken = default);
    }

    public class GetPropertyRwaHandler : IGetPropertyRwaHandler
    {
        private readonly IPropertyQueryService _service;
        public GetPropertyRwaHandler(IPropertyQueryService service) => _service = service;
        public Task<RwaDetailResponseDto?> HandleAsync(GetPropertyRwaQuery query, CancellationToken cancellationToken = default) =>
            _service.GetPropertyRwaAsync(query, cancellationToken);
    }
}
