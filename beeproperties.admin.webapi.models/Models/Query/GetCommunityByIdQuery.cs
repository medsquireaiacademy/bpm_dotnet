namespace beeproperties.admin.webapi.models.Models.Query
{
    public class GetCommunityByIdQuery
    {
        public string Id { get; set; } = string.Empty;

        public GetCommunityByIdQuery() { }

        public GetCommunityByIdQuery(string id)
        {
            Id = id;
        }
    }
}

