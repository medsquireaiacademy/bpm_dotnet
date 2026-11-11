namespace beeproperties.admin.webapi.models.Models.Query
{
    public class GetAgencyByIdQuery
    {
        public string Id { get; set; } = string.Empty;

        public GetAgencyByIdQuery() { }

        public GetAgencyByIdQuery(string id)
        {
            Id = id;
        }
    }
}

