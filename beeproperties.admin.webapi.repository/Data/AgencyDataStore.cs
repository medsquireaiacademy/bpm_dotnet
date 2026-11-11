using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;
using beeproperties.admin.webapi.repository.Entities;

namespace beeproperties.admin.webapi.repository.Data
{
    public interface IAgencyDataStore
    {
        IEnumerable<AgencyEntity> GetAll();
        AgencyEntity? GetById(string id);
        AgencyEntity Add(AgencyEntity entity);
        AgencyEntity? Update(AgencyEntity entity);
        bool Delete(string id);
        bool Exists(string id);
    }

    public class AgencyDataStore : IAgencyDataStore
    {
        private readonly ConcurrentDictionary<string, AgencyEntity> _agencies = new(StringComparer.OrdinalIgnoreCase);

        public AgencyDataStore()
        {
            SeedInitialData();
        }

        public IEnumerable<AgencyEntity> GetAll()
        {
            return _agencies.Values.Where(a => !a.IsDeleted).ToList();
        }

        public AgencyEntity? GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;

            if (_agencies.TryGetValue(id, out var agency) && !agency.IsDeleted)
            {
                return agency;
            }

            return null;
        }

        public AgencyEntity Add(AgencyEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString("N");
            }

            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = null;
            entity.IsDeleted = false;

            _agencies[entity.Id] = entity;
            return entity;
        }

        public AgencyEntity? Update(AgencyEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id)) return null;

            if (!_agencies.TryGetValue(entity.Id, out var existing) || existing.IsDeleted)
            {
                return null;
            }

            entity.CreatedAt = existing.CreatedAt;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = false;

            _agencies[entity.Id] = entity;
            return entity;
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;

            if (_agencies.TryGetValue(id, out var agency) && !agency.IsDeleted)
            {
                agency.IsDeleted = true;
                agency.UpdatedAt = DateTime.UtcNow;
                return true;
            }

            return false;
        }

        public bool Exists(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            return _agencies.TryGetValue(id, out var agency) && !agency.IsDeleted;
        }

        private void SeedInitialData()
        {
            try
            {
                var filePath = FindAgencyFile();
                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    var fileContent = File.ReadAllText(filePath).Trim();
                    if (!fileContent.StartsWith("{"))
                    {
                        fileContent = "{" + fileContent + "}";
                    }

                    var jsonOptions = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        NumberHandling = JsonNumberHandling.AllowReadingFromString
                    };

                    var parsed = JsonSerializer.Deserialize<AgencySeedFile>(fileContent, jsonOptions);
                    if (parsed?.Agencies != null && parsed.Agencies.Count > 0)
                    {
                        foreach (var agency in parsed.Agencies)
                        {
                            agency.CreatedAt = DateTime.UtcNow;
                            agency.IsDeleted = false;
                            _agencies[agency.Id] = agency;
                        }
                        return;
                    }
                }
            }
            catch
            {
                // Fallback to manual seed if file read or parse fails
            }

            SeedDefaultAgencies();
        }

        private static string? FindAgencyFile()
        {
            var searchPaths = new[]
            {
                Path.Combine(Directory.GetCurrentDirectory(), "agency.md"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "agency.md"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "agency.md"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "agency.md"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "agency.md")
            };

            foreach (var path in searchPaths)
            {
                var fullPath = Path.GetFullPath(path);
                if (File.Exists(fullPath)) return fullPath;
            }

            return null;
        }

        private void SeedDefaultAgencies()
        {
            var defaultAgency = new AgencyEntity
            {
                Id = "ag-1",
                Name = "Apex Security & Vigilance Force",
                Category = "Security Services",
                Categories = new List<string> { "Security Services", "General Maintenance" },
                LogoText = "ASVF",
                ContactPerson = "Capt. Raghavan Menon",
                Phone = "+91 9888392041",
                Email = "dispatch@apexsecurity.com",
                AgreementStatus = "Active (Contracted thru Dec 2027)",
                AvailabilityStatus = "Available",
                TotalPersonnel = 24,
                AllocatedPersonnel = 14,
                AvailablePersonnel = 8,
                OnLeavePersonnel = 2,
                ActiveAssignmentsCount = 4,
                RegistrationRefNumber = "REG-2024-ASVF-01",
                RegistrationStatus = "Approved for Property Assignment",
                SubmissionDate = "2024-01-10",
                TermsAccepted = true,
                DeclarationAccepted = true,
                Company = new AgencyCompanyEntity
                {
                    Name = "Apex Security & Vigilance Force",
                    RegisteredCompanyName = "Apex Security & Vigilance Force Private Limited",
                    BusinessType = "Private Limited Company",
                    RegistrationNumber = "REG-2022-ASVF-8891",
                    Gstin = "27AABCU9603R1Z1",
                    Pan = "AABCU9601R",
                    Website = "https://www.asvfservice.com",
                    YearOfEstablishment = 2018,
                    NumberOfEmployees = 34,
                    CompanyEmail = "dispatch@apexsecurity.com",
                    CompanyPhone = "+91 9888392041",
                    AddressLine1 = "100 Service Boulevard, Tech Corridor",
                    AddressLine2 = "Suite 400",
                    Country = "United States",
                    State = "California",
                    City = "Emerald City",
                    PinCode = "94016",
                    Description = "Apex Security & Vigilance Force provides certified infrastructure maintenance.",
                    AuthorizedSignatoryName = "Capt. Raghavan Menon",
                    AuthorizedSignatoryDesignation = "Managing Director"
                },
                CreatedAt = DateTime.UtcNow
            };

            _agencies[defaultAgency.Id] = defaultAgency;
        }

        private class AgencySeedFile
        {
            [JsonPropertyName("agencies")]
            public List<AgencyEntity>? Agencies { get; set; }
        }
    }
}

