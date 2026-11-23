namespace beeproperties.admin.webapi.models.Models.Commands
{
    public class LoginCommand
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class UpdateAdminProfileCommand
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Title { get; set; }
    }

    public class ChangeAdminPasswordCommand
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class CreateActivityCommand
    {
        public string ActivityType { get; set; } = "property";
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Icon { get; set; } = "CheckCircle2";
        public string? EntityId { get; set; }
    }
}
