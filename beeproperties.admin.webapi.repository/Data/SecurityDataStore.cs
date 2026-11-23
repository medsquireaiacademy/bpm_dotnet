using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;
using beeproperties.admin.webapi.repository.Entities;

namespace beeproperties.admin.webapi.repository.Data
{
    public interface ISecurityDataStore
    {
        IEnumerable<SecurityDeploymentEntity> GetAll();
        SecurityDeploymentEntity? GetById(string id);
        SecurityDeploymentEntity Add(SecurityDeploymentEntity entity);
        SecurityDeploymentEntity? Update(SecurityDeploymentEntity entity);
        bool Delete(string id);
    }

    public class SecurityDataStore : ISecurityDataStore
    {
        private readonly ConcurrentDictionary<string, SecurityDeploymentEntity> _deployments = new(StringComparer.OrdinalIgnoreCase);

        public SecurityDataStore()
        {
            SeedInitialData();
        }

        public IEnumerable<SecurityDeploymentEntity> GetAll()
        {
            return _deployments.Values.Where(d => !d.IsDeleted).ToList();
        }

        public SecurityDeploymentEntity? GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;
            if (_deployments.TryGetValue(id, out var dep) && !dep.IsDeleted)
            {
                return dep;
            }
            return null;
        }

        public SecurityDeploymentEntity Add(SecurityDeploymentEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id))
            {
                entity.Id = $"sec-{DateTime.UtcNow.Ticks}";
            }
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = null;
            entity.IsDeleted = false;

            _deployments[entity.Id] = entity;
            return entity;
        }

        public SecurityDeploymentEntity? Update(SecurityDeploymentEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id)) return null;
            if (!_deployments.TryGetValue(entity.Id, out var existing) || existing.IsDeleted)
            {
                return null;
            }

            entity.CreatedAt = existing.CreatedAt;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = false;

            _deployments[entity.Id] = entity;
            return entity;
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            if (_deployments.TryGetValue(id, out var dep) && !dep.IsDeleted)
            {
                dep.IsDeleted = true;
                dep.UpdatedAt = DateTime.UtcNow;
                return true;
            }
            return false;
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
                    if (doc.RootElement.TryGetProperty("securityDeployments", out var secElement))
                    {
                        var parsed = JsonSerializer.Deserialize<List<SecurityDeploymentEntity>>(secElement.GetRawText(), jsonOptions);
                        if (parsed != null && parsed.Count > 0)
                        {
                            foreach (var s in parsed)
                            {
                                s.CreatedAt = DateTime.UtcNow;
                                s.IsDeleted = false;
                                _deployments[s.Id] = s;
                            }
                            return;
                        }
                    }
                }
            }
            catch
            {
            }

            var d1 = new SecurityDeploymentEntity
            {
                Id = "sec-1",
                ApartmentId = "apt-1",
                ApartmentName = "Bee Royal Heights",
                AgencyId = "ag-1",
                AgencyName = "Apex Security & Vigilance Force",
                BlockName = "Main Gate & Perimeter",
                Shift = "Morning Shift (06:00 - 14:00)",
                RequiredGuards = 2,
                AllocatedGuards = 2,
                PersonnelNames = new List<string> { "Vikram Singh", "Balwan Yadav" },
                StartDate = "2024-01-15",
                Status = "Active"
            };
            _deployments[d1.Id] = d1;
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
    }
}
