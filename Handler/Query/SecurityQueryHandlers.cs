using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.Handler.Query
{
    public interface IGetAllSecurityDeploymentsHandler
    {
        Task<List<SecurityDeploymentDto>> HandleAsync(GetAllSecurityDeploymentsQuery query, CancellationToken cancellationToken = default);
    }

    public class GetAllSecurityDeploymentsHandler : IGetAllSecurityDeploymentsHandler
    {
        private readonly ISecurityQueryService _service;
        public GetAllSecurityDeploymentsHandler(ISecurityQueryService service) => _service = service;
        public Task<List<SecurityDeploymentDto>> HandleAsync(GetAllSecurityDeploymentsQuery query, CancellationToken cancellationToken = default) =>
            _service.GetAllDeploymentsAsync(query, cancellationToken);
    }

    public interface IGetSecurityDeploymentByIdHandler
    {
        Task<SecurityDeploymentDto?> HandleAsync(GetSecurityDeploymentByIdQuery query, CancellationToken cancellationToken = default);
    }

    public class GetSecurityDeploymentByIdHandler : IGetSecurityDeploymentByIdHandler
    {
        private readonly ISecurityQueryService _service;
        public GetSecurityDeploymentByIdHandler(ISecurityQueryService service) => _service = service;
        public Task<SecurityDeploymentDto?> HandleAsync(GetSecurityDeploymentByIdQuery query, CancellationToken cancellationToken = default) =>
            _service.GetDeploymentByIdAsync(query, cancellationToken);
    }
}
