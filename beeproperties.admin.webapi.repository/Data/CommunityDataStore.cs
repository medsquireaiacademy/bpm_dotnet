using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;
using beeproperties.admin.webapi.repository.Entities;

namespace beeproperties.admin.webapi.repository.Data
{
    public interface ICommunityDataStore
    {
        IEnumerable<CommunityEntity> GetAll();
        CommunityEntity? GetById(string id);
        CommunityEntity Add(CommunityEntity entity);
        CommunityEntity? Update(CommunityEntity entity);
        bool Delete(string id);
        bool Exists(string id);
    }

    public class CommunityDataStore : ICommunityDataStore
    {
        private readonly ConcurrentDictionary<string, CommunityEntity> _communities = new(StringComparer.OrdinalIgnoreCase);

        public CommunityDataStore()
        {
            SeedInitialData();
        }

        public IEnumerable<CommunityEntity> GetAll()
        {
            return _communities.Values.Where(c => !c.IsDeleted).ToList();
        }

        public CommunityEntity? GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;

            if (_communities.TryGetValue(id, out var community) && !community.IsDeleted)
            {
                return community;
            }

            return null;
        }

        public CommunityEntity Add(CommunityEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString("N");
            }

            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = null;
            entity.IsDeleted = false;

            _communities[entity.Id] = entity;
            return entity;
        }

        public CommunityEntity? Update(CommunityEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id)) return null;

            if (!_communities.TryGetValue(entity.Id, out var existing) || existing.IsDeleted)
            {
                return null;
            }

            entity.CreatedAt = existing.CreatedAt;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = false;

            _communities[entity.Id] = entity;
            return entity;
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;

            if (_communities.TryGetValue(id, out var community) && !community.IsDeleted)
            {
                community.IsDeleted = true;
                community.UpdatedAt = DateTime.UtcNow;
                return true;
            }

            return false;
        }

        public bool Exists(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            return _communities.TryGetValue(id, out var community) && !community.IsDeleted;
        }

        private void SeedInitialData()
        {
            try
            {
                var filePath = FindCommunityFile();
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

                    var parsed = JsonSerializer.Deserialize<CommunitySeedFile>(fileContent, jsonOptions);
                    if (parsed?.Entries?.CommunityInfo != null)
                    {
                        var info = parsed.Entries.CommunityInfo;
                        var community = new CommunityEntity
                        {
                            Id = string.IsNullOrWhiteSpace(info.Id) ? "bee-community-master" : info.Id,
                            Name = info.Name,
                            Tagline = info.Tagline,
                            Description = info.Description,
                            Address = info.Address,
                            City = info.City,
                            State = info.State,
                            Zip = info.Zip,
                            AerialImage = info.AerialImage,
                            TotalApartments = info.TotalApartments,
                            TotalBlocks = info.TotalBlocks,
                            TotalFloors = info.TotalFloors,
                            TotalFlats = info.TotalFlats,
                            Valuation = info.Valuation,
                            Established = info.Established,
                            Amenities = info.Amenities ?? new List<CommunityAmenityEntity>(),
                            Apartments = parsed.Entries.ApartmentTemplates ?? new List<ApartmentTemplateEntity>(),
                            Flats = parsed.Entries.InitialCommunityFlats ?? new List<CommunityFlatEntity>(),
                            CreatedAt = DateTime.UtcNow
                        };

                        _communities[community.Id] = community;
                        return;
                    }
                }
            }
            catch
            {
                // Fallback to manual seed if file read or parse fails
            }

            SeedDefaultMasterCommunity();
        }

        private static string? FindCommunityFile()
        {
            var searchPaths = new[]
            {
                Path.Combine(Directory.GetCurrentDirectory(), "Community.md"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Community.md"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "Community.md"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Community.md"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "Community.md")
            };

            foreach (var path in searchPaths)
            {
                var fullPath = Path.GetFullPath(path);
                if (File.Exists(fullPath)) return fullPath;
            }

            return null;
        }

        private void SeedDefaultMasterCommunity()
        {
            var defaultCommunity = new CommunityEntity
            {
                Id = "bee-community-master",
                Name = "Bee Property Community",
                Tagline = "Premier Master-Planned Residential Community",
                Description = "An elite 45-acre integrated gated development comprising 3 luxury apartment complexes, 9 residential blocks, 36 floors, and 144 designer flats.",
                Address = "Bee Central Boulevard & Honeycomb Way, Metro Emerald District",
                City = "Emerald City",
                State = "CA",
                Zip = "94016",
                AerialImage = "/Images/bee-community-master.jpg",
                TotalApartments = 3,
                TotalBlocks = 9,
                TotalFloors = 36,
                TotalFlats = 144,
                Valuation = "$84.5M",
                Established = 2024,
                CreatedAt = DateTime.UtcNow
            };

            _communities[defaultCommunity.Id] = defaultCommunity;
        }

        private class CommunitySeedFile
        {
            public CommunitySeedEntries? Entries { get; set; }
        }

        private class CommunitySeedEntries
        {
            [JsonPropertyName("COMMUNITY_INFO")]
            public CommunityInfoSeed? CommunityInfo { get; set; }

            [JsonPropertyName("APARTMENT_TEMPLATES")]
            public List<ApartmentTemplateEntity>? ApartmentTemplates { get; set; }

            [JsonPropertyName("INITIAL_COMMUNITY_FLATS")]
            public List<CommunityFlatEntity>? InitialCommunityFlats { get; set; }
        }

        private class CommunityInfoSeed
        {
            public string Id { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Tagline { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
            public string State { get; set; } = string.Empty;
            public string Zip { get; set; } = string.Empty;
            public string AerialImage { get; set; } = string.Empty;
            public int TotalApartments { get; set; }
            public int TotalBlocks { get; set; }
            public int TotalFloors { get; set; }
            public int TotalFlats { get; set; }
            public string Valuation { get; set; } = string.Empty;
            public int Established { get; set; }
            public List<CommunityAmenityEntity>? Amenities { get; set; }
        }
    }
}

