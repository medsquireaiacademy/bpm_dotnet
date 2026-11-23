namespace beeproperties.admin.webapi.models.Models.Query
{
    public class GetAllSecurityDeploymentsQuery
    {
        public string? PropertyId { get; set; }
        public string? AgencyId { get; set; }
        public string? Status { get; set; }
    }

    public class GetSecurityDeploymentByIdQuery
    {
        public string Id { get; set; }

        public GetSecurityDeploymentByIdQuery(string id)
        {
            Id = id;
        }
    }
}
