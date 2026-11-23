namespace beeproperties.admin.webapi.repository.Entities
{
    public class AdminUserEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Name { get => FullName; set => FullName = value; }
        public string Role { get; set; } = "Super Admin";
        public string Initials { get; set; } = "SJ";
        public string Phone { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }

    public class UserTokenEntity
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class ActivityEntity
    {
        public string Id { get; set; } = string.Empty;
        public string ActivityType { get; set; } = "property";
        public string Type { get => ActivityType; set => ActivityType = value; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Time { get; set; } = "Just now";
        public string Icon { get; set; } = "CheckCircle2";
        public string? EntityId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
