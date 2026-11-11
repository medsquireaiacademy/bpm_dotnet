namespace beeproperties.admin.webapi.repository.Entities
{
    public class AgencyDocumentEntity
    {
        public string Id { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string UploadDate { get; set; } = string.Empty;
        public string? ExpiryDate { get; set; }
        public string VerificationStatus { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
    }
}

