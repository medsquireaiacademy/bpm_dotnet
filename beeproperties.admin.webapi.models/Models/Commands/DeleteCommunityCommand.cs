namespace beeproperties.admin.webapi.models.Models.Commands
{
    public class DeleteCommunityCommand
    {
        public string Id { get; set; } = string.Empty;

        public DeleteCommunityCommand() { }

        public DeleteCommunityCommand(string id)
        {
            Id = id;
        }
    }
}

