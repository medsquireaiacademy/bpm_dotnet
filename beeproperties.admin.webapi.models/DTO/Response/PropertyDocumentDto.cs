namespace beeproperties.admin.webapi.models.DTO.Response
{
    public class PropertyDocumentDto
    {
        public string Id { get; set; } = string.Empty;
        public string PropertyId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public string FileSize { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        public string Description { get; set; } = string.Empty;
        public string UploadDate { get; set; } = string.Empty;
    }

    public class RwaDetailResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
        public string Name { get; set; } = string.Empty;
        public string RegNumber { get; set; } = string.Empty;
        public string RegDate { get; set; } = string.Empty;
        public string President { get; set; } = string.Empty;
        public string Secretary { get; set; } = string.Empty;
        public string Treasurer { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string OfficeHours { get; set; } = string.Empty;
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int TotalMembers { get; set; }
        public List<RwaCommitteeMemberDto> Committee { get; set; } = new();
    }

    public class RwaCommitteeMemberDto
    {
        public string Id { get; set; } = string.Empty;
        public string PropertyId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Flat { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
    }
}
