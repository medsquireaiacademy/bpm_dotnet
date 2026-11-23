using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;
using beeproperties.admin.webapi.repository.Entities;

namespace beeproperties.admin.webapi.repository.Data
{
    public interface IPropertyDataStore
    {
        IEnumerable<PropertyEntity> GetAll();
        PropertyEntity? GetById(string id);
        PropertyEntity Add(PropertyEntity entity);
        PropertyEntity? Update(PropertyEntity entity);
        bool Delete(string id);
        bool Exists(string id);
    }

    public class PropertyDataStore : IPropertyDataStore
    {
        private readonly ConcurrentDictionary<string, PropertyEntity> _properties = new(StringComparer.OrdinalIgnoreCase);

        public PropertyDataStore()
        {
            SeedInitialData();
        }

        public IEnumerable<PropertyEntity> GetAll()
        {
            return _properties.Values.Where(p => !p.IsDeleted).ToList();
        }

        public PropertyEntity? GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;
            if (_properties.TryGetValue(id, out var prop) && !prop.IsDeleted)
            {
                return prop;
            }
            return null;
        }

        public PropertyEntity Add(PropertyEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id))
            {
                entity.Id = $"apt-{DateTime.UtcNow.Ticks}";
            }
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = null;
            entity.IsDeleted = false;
            RecalculateCounters(entity);
            _properties[entity.Id] = entity;
            return entity;
        }

        public PropertyEntity? Update(PropertyEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id)) return null;
            if (!_properties.TryGetValue(entity.Id, out var existing) || existing.IsDeleted)
            {
                return null;
            }

            entity.CreatedAt = existing.CreatedAt;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = false;
            RecalculateCounters(entity);
            _properties[entity.Id] = entity;
            return entity;
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            if (_properties.TryGetValue(id, out var prop) && !prop.IsDeleted)
            {
                prop.IsDeleted = true;
                prop.UpdatedAt = DateTime.UtcNow;
                return true;
            }
            return false;
        }

        public bool Exists(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            return _properties.TryGetValue(id, out var prop) && !prop.IsDeleted;
        }

        private static void RecalculateCounters(PropertyEntity prop)
        {
            int totalBlocks = prop.Blocks?.Count ?? 0;
            int totalFloors = 0;
            int totalFlats = 0;
            int occupiedFlats = 0;
            int vacantFlats = 0;

            if (prop.Blocks != null)
            {
                foreach (var block in prop.Blocks)
                {
                    int bFloors = block.Floors?.Count ?? 0;
                    totalFloors += bFloors;
                    int bFlats = 0;
                    int bOccupied = 0;
                    int bVacant = 0;

                    if (block.Floors != null)
                    {
                        foreach (var floor in block.Floors)
                        {
                            int fFlats = floor.Flats?.Count ?? 0;
                            int fOcc = floor.Flats?.Count(f => string.Equals(f.Status, "Occupied", StringComparison.OrdinalIgnoreCase)) ?? 0;
                            int fVac = fFlats - fOcc;

                            floor.TotalFlats = fFlats;
                            floor.OccupiedFlats = fOcc;
                            floor.VacantFlats = fVac;

                            bFlats += fFlats;
                            bOccupied += fOcc;
                            bVacant += fVac;
                        }
                    }

                    block.FloorsCount = bFloors;
                    block.FlatsCount = bFlats;
                    block.OccupiedFlats = bOccupied;
                    block.VacantFlats = bVacant;

                    totalFlats += bFlats;
                    occupiedFlats += bOccupied;
                    vacantFlats += bVacant;
                }
            }

            prop.TotalBlocks = totalBlocks;
            prop.TotalFloors = totalFloors;
            prop.TotalFlats = totalFlats;
            prop.OccupiedFlats = occupiedFlats;
            prop.VacantFlats = vacantFlats;
        }

        private void SeedInitialData()
        {
            try
            {
                var filePath = FindApplicationDataFile();
                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    var fileContent = File.ReadAllText(filePath).Trim();
                    var jsonOptions = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        NumberHandling = JsonNumberHandling.AllowReadingFromString
                    };

                    using var doc = JsonDocument.Parse(fileContent);
                    if (doc.RootElement.TryGetProperty("apartments", out var aptsElement))
                    {
                        var parsedApts = JsonSerializer.Deserialize<List<PropertyEntity>>(aptsElement.GetRawText(), jsonOptions);
                        if (parsedApts != null && parsedApts.Count > 0)
                        {
                            foreach (var p in parsedApts)
                            {
                                p.CreatedAt = DateTime.UtcNow;
                                p.IsDeleted = false;
                                RecalculateCounters(p);
                                _properties[p.Id] = p;
                            }
                            return;
                        }
                    }
                }
            }
            catch
            {
                // Fallback to manual seed if file read fails
            }

            SeedDefaultProperties();
        }

        private static string? FindApplicationDataFile()
        {
            var searchPaths = new[]
            {
                Path.Combine(Directory.GetCurrentDirectory(), "complete_application_data.json"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "Admin_bee_web", "complete_application_data.json"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "Admin_bee_web", "complete_application_data.json"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "complete_application_data.json")
            };

            foreach (var path in searchPaths)
            {
                var fullPath = Path.GetFullPath(path);
                if (File.Exists(fullPath)) return fullPath;
            }

            return null;
        }

        private void SeedDefaultProperties()
        {
            var p1 = new PropertyEntity
            {
                Id = "apt-1",
                Code = "BRH",
                Name = "Bee Royal Heights",
                Tagline = "Flagship Luxury Gated Community",
                Address = "Plot 18 & 19, Financial District, Nanakramguda",
                City = "Hyderabad",
                State = "Telangana",
                Zip = "500032",
                Country = "India",
                Image = "/Images/bee-community-master.jpg",
                BuiltYear = 2024,
                Status = "Active - Optimal",
                LandArea = "14.5 Acres",
                Description = "A premier luxury high-rise gated apartment enclave featuring world-class amenities.",
                Rating = 4.9,
                AccentColor = "#F59E0B",
                RwaStatus = "Active",
                Rwa = new PropertyRwaEntity
                {
                    Status = "Active",
                    Name = "Bee Royal Heights Residents' Welfare Association",
                    RegNumber = "RWA-2024-HYD-0892",
                    RegDate = "2024-03-15",
                    President = "Col. Sanjeev Varma (Retd.)",
                    Secretary = "Dr. Ananya Iyer",
                    Treasurer = "Vikramaditya Shah, CA",
                    ContactPhone = "+91 9844829901",
                    ContactEmail = "rwa.executive@beeroyalheights.org",
                    OfficeHours = "Mon - Sat: 09:00 AM - 01:00 PM",
                    TotalMembers = 7,
                    Committee = new List<PropertyRwaMemberEntity>
                    {
                        new() { Id = "m-1", Name = "Meera Nair", Role = "Joint Secretary", Flat = "A-201", Phone = "+91 9844829902" }
                    }
                },
                Blocks = new List<PropertyBlockEntity>
                {
                    new()
                    {
                        Id = "apt-1-B1",
                        Code = "Block-A",
                        Name = "Block A (Royal Hive)",
                        Wing = "North Wing",
                        Elevators = 2,
                        PowerBackup = "100% DG Backed",
                        Security = "Biometric FaceID + RFID",
                        Status = "Operational",
                        Floors = new List<PropertyFloorEntity>
                        {
                            new()
                            {
                                Id = "apt-1-B1-F1",
                                FloorNumber = 1,
                                Name = "Floor 1",
                                Flats = new List<PropertyFlatEntity>
                                {
                                    new() { Id = "f-101", Number = "101", Status = "Occupied", ResidentType = "Owner", ResidentName = "Amit Verma", Phone = "+91 9876543210" },
                                    new() { Id = "f-102", Number = "102", Status = "Vacant" }
                                }
                            }
                        }
                    }
                }
            };
            RecalculateCounters(p1);
            _properties[p1.Id] = p1;
        }
    }
}
