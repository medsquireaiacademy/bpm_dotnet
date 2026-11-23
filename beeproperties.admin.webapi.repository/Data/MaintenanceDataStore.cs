using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;
using beeproperties.admin.webapi.repository.Entities;

namespace beeproperties.admin.webapi.repository.Data
{
    public interface IMaintenanceDataStore
    {
        IEnumerable<MaintenanceRequestEntity> GetAll();
        MaintenanceRequestEntity? GetById(string id);
        MaintenanceRequestEntity Add(MaintenanceRequestEntity entity);
        MaintenanceRequestEntity? Update(MaintenanceRequestEntity entity);
        bool Delete(string id);
        bool Exists(string id);
    }

    public class MaintenanceDataStore : IMaintenanceDataStore
    {
        private readonly ConcurrentDictionary<string, MaintenanceRequestEntity> _requests = new(StringComparer.OrdinalIgnoreCase);

        public MaintenanceDataStore()
        {
            SeedInitialData();
        }

        public IEnumerable<MaintenanceRequestEntity> GetAll()
        {
            return _requests.Values.Where(r => !r.IsDeleted).ToList();
        }

        public MaintenanceRequestEntity? GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;
            if (_requests.TryGetValue(id, out var req) && !req.IsDeleted)
            {
                return req;
            }
            return null;
        }

        public MaintenanceRequestEntity Add(MaintenanceRequestEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id))
            {
                entity.Id = $"maint-{DateTime.UtcNow.Ticks}";
            }
            if (string.IsNullOrWhiteSpace(entity.TicketNumber))
            {
                entity.TicketNumber = $"MNT-{DateTime.UtcNow:yyyyMMdd}-{_requests.Count + 101}";
            }
            if (string.IsNullOrWhiteSpace(entity.ReportedDate))
            {
                entity.ReportedDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
            }
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = null;
            entity.IsDeleted = false;

            _requests[entity.Id] = entity;
            return entity;
        }

        public MaintenanceRequestEntity? Update(MaintenanceRequestEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id)) return null;
            if (!_requests.TryGetValue(entity.Id, out var existing) || existing.IsDeleted)
            {
                return null;
            }

            entity.CreatedAt = existing.CreatedAt;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = false;

            _requests[entity.Id] = entity;
            return entity;
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            if (_requests.TryGetValue(id, out var req) && !req.IsDeleted)
            {
                req.IsDeleted = true;
                req.UpdatedAt = DateTime.UtcNow;
                return true;
            }
            return false;
        }

        public bool Exists(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            return _requests.TryGetValue(id, out var req) && !req.IsDeleted;
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
                    if (doc.RootElement.TryGetProperty("maintenanceRequests", out var maintElement))
                    {
                        var parsed = JsonSerializer.Deserialize<List<MaintenanceRequestEntity>>(maintElement.GetRawText(), jsonOptions);
                        if (parsed != null && parsed.Count > 0)
                        {
                            foreach (var r in parsed)
                            {
                                r.CreatedAt = DateTime.UtcNow;
                                r.IsDeleted = false;
                                if (string.IsNullOrWhiteSpace(r.TicketNumber))
                                {
                                    r.TicketNumber = $"MNT-{r.Id.Replace("maint-", "")}";
                                }
                                _requests[r.Id] = r;
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

            SeedDefaultRequests();
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

        private void SeedDefaultRequests()
        {
            var r1 = new MaintenanceRequestEntity
            {
                Id = "maint-101",
                TicketNumber = "MNT-2026-0101",
                Title = "Variable Refrigerant Flow (VRF) AC Unit Failure",
                Description = "Master bedroom VRF indoor unit showing error code E-04 with condensate pump drainage fault.",
                Category = "HVAC / Air Conditioning",
                ApartmentId = "apt-1",
                ApartmentName = "Bee Royal Heights",
                BlockId = "apt-1-B1",
                BlockName = "Block A (Royal Hive)",
                FloorId = "apt-1-B1-F1",
                FlatNumber = "101",
                LocationDescription = "Block A, Flat 101 Master Bedroom",
                Priority = "High",
                Status = "Assigned",
                ReportedDate = "2026-10-22",
                AssignedAgencyId = "ag-2",
                AssignedAgencyName = "CoolTech Climate Systems",
                AssignedPersonnel = new List<string> { "Sanjay Rawat" },
                ScheduledDate = "2026-10-26",
                ScheduledTime = "14:00 - 17:00 (Afternoon Slot)"
            };
            _requests[r1.Id] = r1;
        }
    }
}
