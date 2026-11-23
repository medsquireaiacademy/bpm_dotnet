using System.Collections.Concurrent;
using beeproperties.admin.webapi.repository.Entities;

namespace beeproperties.admin.webapi.repository.Data
{
    public interface IAuthDataStore
    {
        AdminUserEntity? GetUserByUsername(string username);
        AdminUserEntity? GetUserById(string id);
        AdminUserEntity UpdateUser(AdminUserEntity user);
        IEnumerable<ActivityEntity> GetRecentActivities(int limit, string? type = null);
        void AddActivity(ActivityEntity activity);
    }

    public class AuthDataStore : IAuthDataStore
    {
        private readonly ConcurrentDictionary<string, AdminUserEntity> _users = new(StringComparer.OrdinalIgnoreCase);
        private readonly ConcurrentBag<ActivityEntity> _activities = new();

        public AuthDataStore()
        {
            SeedDefaults();
        }

        public AdminUserEntity? GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return null;
            return _users.Values.FirstOrDefault(u =>
                string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(u.Email, username, StringComparison.OrdinalIgnoreCase));
        }

        public AdminUserEntity? GetUserById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;
            _users.TryGetValue(id, out var user);
            return user;
        }

        public AdminUserEntity UpdateUser(AdminUserEntity user)
        {
            user.UpdatedAt = DateTime.UtcNow;
            _users[user.Id] = user;
            return user;
        }

        public IEnumerable<ActivityEntity> GetRecentActivities(int limit, string? type = null)
        {
            var q = _activities.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(type))
            {
                q = q.Where(a => string.Equals(a.ActivityType, type, StringComparison.OrdinalIgnoreCase));
            }
            return q.OrderByDescending(a => a.Timestamp).Take(limit).ToList();
        }

        public void AddActivity(ActivityEntity activity)
        {
            if (string.IsNullOrWhiteSpace(activity.Id))
            {
                activity.Id = $"act-{DateTime.UtcNow.Ticks}";
            }
            _activities.Add(activity);
        }

        private void SeedDefaults()
        {
            var admin = new AdminUserEntity
            {
                Id = "usr-1",
                Username = "admin",
                Email = "sarah.jenkins@beeproperty.com",
                FullName = "Sarah Jenkins",
                Role = "Super Admin",
                Initials = "SJ",
                Phone = "+91 9876543210",
                Title = "Super Admin",
                PasswordHash = "admin123", // Matches default demo credentials
                IsActive = true
            };
            _users[admin.Id] = admin;

            // Seed initial activity events matching Admin_bee_web
            AddActivity(new ActivityEntity
            {
                Id = "act-1",
                ActivityType = "assignment",
                Title = "Maintenance Agency Allocated",
                Description = "CoolTech Climate Systems assigned to VRF AC Unit Failure at Bee Royal Heights.",
                Time = "10 mins ago",
                Icon = "Wrench",
                Timestamp = DateTime.UtcNow.AddMinutes(-10)
            });
            AddActivity(new ActivityEntity
            {
                Id = "act-2",
                ActivityType = "security",
                Title = "Security Shift Deployed",
                Description = "Apex Security deployed 2 officers at Honeycomb Towers North Gate.",
                Time = "1 hour ago",
                Icon = "ShieldCheck",
                Timestamp = DateTime.UtcNow.AddHours(-1)
            });
            AddActivity(new ActivityEntity
            {
                Id = "act-3",
                ActivityType = "property",
                Title = "New Property Registered",
                Description = "\"Nectar Bay Living\" (NBL) registered with 3 blocks.",
                Time = "3 hours ago",
                Icon = "Building2",
                Timestamp = DateTime.UtcNow.AddHours(-3)
            });
        }
    }
}
