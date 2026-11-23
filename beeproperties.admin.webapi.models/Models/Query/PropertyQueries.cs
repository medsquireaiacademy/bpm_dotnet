namespace beeproperties.admin.webapi.models.Models.Query
{
    public class GetAllPropertiesQuery
    {
        public string? SearchTerm { get; set; }
        public string? Status { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PropertyType { get; set; }
    }

    public class GetPropertyByIdQuery
    {
        public string Id { get; set; }

        public GetPropertyByIdQuery(string id)
        {
            Id = id;
        }
    }

    public class GetPropertySummaryQuery
    {
        public string? PropertyId { get; set; }

        public GetPropertySummaryQuery(string? propertyId = null)
        {
            PropertyId = propertyId;
        }
    }

    public class GetPropertyBlocksQuery
    {
        public string PropertyId { get; set; }

        public GetPropertyBlocksQuery(string propertyId)
        {
            PropertyId = propertyId;
        }
    }

    public class GetBlockFloorsQuery
    {
        public string PropertyId { get; set; }
        public string BlockId { get; set; }

        public GetBlockFloorsQuery(string propertyId, string blockId)
        {
            PropertyId = propertyId;
            BlockId = blockId;
        }
    }

    public class GetFloorFlatsQuery
    {
        public string PropertyId { get; set; }
        public string BlockId { get; set; }
        public string FloorId { get; set; }

        public GetFloorFlatsQuery(string propertyId, string blockId, string floorId)
        {
            PropertyId = propertyId;
            BlockId = blockId;
            FloorId = floorId;
        }
    }

    public class GetFlatByNumberQuery
    {
        public string PropertyId { get; set; }
        public string FlatNumber { get; set; }

        public GetFlatByNumberQuery(string propertyId, string flatNumber)
        {
            PropertyId = propertyId;
            FlatNumber = flatNumber;
        }
    }

    public class GetPropertyDocumentsQuery
    {
        public string PropertyId { get; set; }
        public string? Category { get; set; }

        public GetPropertyDocumentsQuery(string propertyId, string? category = null)
        {
            PropertyId = propertyId;
            Category = category;
        }
    }

    public class GetPropertyRwaQuery
    {
        public string PropertyId { get; set; }

        public GetPropertyRwaQuery(string propertyId)
        {
            PropertyId = propertyId;
        }
    }
}
