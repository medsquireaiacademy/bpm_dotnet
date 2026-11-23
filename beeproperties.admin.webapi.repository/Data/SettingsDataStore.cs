using System.Collections.Concurrent;
using beeproperties.admin.webapi.repository.Entities;

namespace beeproperties.admin.webapi.repository.Data
{
    public interface ISettingsDataStore
    {
        IEnumerable<ServiceCategoryEntity> GetServiceCategories();
        ServiceCategoryEntity? GetServiceCategoryById(string id);
        ServiceCategoryEntity AddServiceCategory(ServiceCategoryEntity entity);
        ServiceCategoryEntity? UpdateServiceCategory(ServiceCategoryEntity entity);
        bool DeleteServiceCategory(string id);

        List<string> GetLookupValues(string group);
        void SetLookupValues(string group, List<string> values);

        AppPreferencesEntity GetPreferences();
        AppPreferencesEntity UpdatePreferences(AppPreferencesEntity entity);
        void ResetAll();
    }

    public class SettingsDataStore : ISettingsDataStore
    {
        private readonly ConcurrentDictionary<string, ServiceCategoryEntity> _serviceCategories = new(StringComparer.OrdinalIgnoreCase);
        private readonly ConcurrentDictionary<string, List<string>> _lookups = new(StringComparer.OrdinalIgnoreCase);
        private AppPreferencesEntity _preferences = new();

        public SettingsDataStore()
        {
            SeedDefaults();
        }

        public IEnumerable<ServiceCategoryEntity> GetServiceCategories()
        {
            return _serviceCategories.Values.Where(c => !c.IsDeleted).ToList();
        }

        public ServiceCategoryEntity? GetServiceCategoryById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;
            if (_serviceCategories.TryGetValue(id, out var cat) && !cat.IsDeleted)
            {
                return cat;
            }
            return null;
        }

        public ServiceCategoryEntity AddServiceCategory(ServiceCategoryEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id))
            {
                entity.Id = $"cat-{DateTime.UtcNow.Ticks}";
            }
            entity.CreatedAt = DateTime.UtcNow;
            entity.IsDeleted = false;
            _serviceCategories[entity.Id] = entity;
            return entity;
        }

        public ServiceCategoryEntity? UpdateServiceCategory(ServiceCategoryEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id)) return null;
            if (!_serviceCategories.TryGetValue(entity.Id, out var existing) || existing.IsDeleted)
            {
                return null;
            }

            entity.CreatedAt = existing.CreatedAt;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = false;
            _serviceCategories[entity.Id] = entity;
            return entity;
        }

        public bool DeleteServiceCategory(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            if (_serviceCategories.TryGetValue(id, out var cat) && !cat.IsDeleted)
            {
                cat.IsDeleted = true;
                cat.UpdatedAt = DateTime.UtcNow;
                return true;
            }
            return false;
        }

        public List<string> GetLookupValues(string group)
        {
            if (_lookups.TryGetValue(group, out var list))
            {
                return list.ToList();
            }
            return new List<string>();
        }

        public void SetLookupValues(string group, List<string> values)
        {
            _lookups[group] = values.ToList();
        }

        public AppPreferencesEntity GetPreferences()
        {
            return _preferences;
        }

        public AppPreferencesEntity UpdatePreferences(AppPreferencesEntity entity)
        {
            _preferences = entity;
            return _preferences;
        }

        public void ResetAll()
        {
            _serviceCategories.Clear();
            _lookups.Clear();
            SeedDefaults();
        }

        private void SeedDefaults()
        {
            var defaultCategories = new[]
            {
                "Housekeeping and Cleaning", "Security Services", "Facility Management",
                "Electrical Maintenance", "Plumbing", "HVAC / Air Conditioning",
                "Fire Safety and Fire Protection Systems", "Lift / Elevator Maintenance",
                "DG Set / Generator Maintenance", "STP / WTP / Water Treatment",
                "Landscaping and Gardening", "Pest Control", "Civil Works and Carpentry",
                "Painting and Waterproofing", "IT, Networking and CCTV",
                "Access Control and Intercom", "Waste Management", "Swimming Pool Maintenance",
                "Gym / Clubhouse Equipment Maintenance", "Appliance Repair",
                "Moving and Relocation", "Property Leasing / Real Estate Services", "Other"
            };

            int i = 1;
            foreach (var cat in defaultCategories)
            {
                var id = $"cat-{i++}";
                _serviceCategories[id] = new ServiceCategoryEntity
                {
                    Id = id,
                    Name = cat,
                    Description = $"Standard service category: {cat}",
                    Status = "Active"
                };
            }

            _lookups["PropertyTypes"] = new List<string>
            {
                "Residential Apartment", "Gated Community", "Residential Township", "Other"
            };

            _lookups["PropertyStatuses"] = new List<string>
            {
                "Active - Optimal", "Active - Maintenance Due", "Under Expansion", "Under Inspection"
            };

            _lookups["BusinessTypes"] = new List<string>
            {
                "Sole Proprietorship", "Partnership", "LLP", "Private Limited Company",
                "Public Limited Company", "Individual / Independent Service Provider", "Other"
            };

            _lookups["AgentTypes"] = new List<string>
            {
                "Company Owner", "Authorized Representative", "Employee",
                "Field Agent / Technician", "Service Coordinator", "Other"
            };

            _lookups["PreferredContactMethods"] = new List<string>
            {
                "Phone Call", "WhatsApp", "Email", "Bee App Notification"
            };

            _lookups["RegistrationStatuses"] = new List<string>
            {
                "Draft", "Submitted", "Under Review", "Changes Requested",
                "Approved for Property Assignment", "Rejected", "Suspended"
            };

            _lookups["AssignmentTypes"] = new List<string>
            {
                "Single Property", "Multiple Properties", "Property Portfolio",
                "Temporary / Project-Based Assignment"
            };

            _lookups["AssignmentStatuses"] = new List<string>
            {
                "Draft", "Submitted for Approval", "Under Review", "Changes Requested",
                "Approved", "Approved with Restrictions", "Rejected", "Suspended", "Expired", "Revoked"
            };

            _lookups["ApprovalDecisions"] = new List<string>
            {
                "Approve", "Approve with Restrictions", "Request Changes", "Reject"
            };

            _lookups["PropertyDocumentCategories"] = new List<string>
            {
                "Master Plan", "Apartment Layout", "Floor Plans", "Route Maps", "Site Maps",
                "Construction Plans", "Building Approval Documents", "Completion Certificates",
                "Occupancy Certificates", "Fire Safety Approvals", "Maintenance Manuals",
                "Other Supporting Documents"
            };

            _lookups["AgencyDocumentTypes"] = new List<string>
            {
                "Company Registration Certificate", "GST Registration Certificate", "PAN Document",
                "Company / Business Profile", "Authorized Signatory ID / Authorization Letter",
                "Service Certifications / Licences", "Insurance Certificate",
                "Previous Work / Experience Portfolio", "Other Supporting Documents"
            };
        }
    }
}
