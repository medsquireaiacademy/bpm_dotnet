namespace beeproperties.admin.webapi.models.Models.Commands
{
    public class DeleteAgencyCommand
    {
        public string Id { get; set; } = string.Empty;

        public DeleteAgencyCommand() { }

        public DeleteAgencyCommand(string id)
        {
            Id = id;
        }
    }
}

