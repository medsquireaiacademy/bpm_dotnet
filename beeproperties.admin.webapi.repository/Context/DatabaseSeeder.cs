using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using beeproperties.admin.webapi.repository.Data;

namespace beeproperties.admin.webapi.repository.Context
{
    public interface IDatabaseSeeder
    {
        Task InitializeAsync(CancellationToken cancellationToken = default);
    }

    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly BeePropertiesDbContext _dbContext;
        private readonly IPropertyDataStore _propertyDataStore;
        private readonly IAgencyDataStore _agencyDataStore;
        private readonly IMaintenanceDataStore _maintenanceDataStore;
        private readonly ISettingsDataStore _settingsDataStore;
        private readonly IAuthDataStore _authDataStore;
        private readonly ILogger<DatabaseSeeder> _logger;

        public DatabaseSeeder(
            BeePropertiesDbContext dbContext,
            IPropertyDataStore propertyDataStore,
            IAgencyDataStore agencyDataStore,
            IMaintenanceDataStore maintenanceDataStore,
            ISettingsDataStore settingsDataStore,
            IAuthDataStore authDataStore,
            ILogger<DatabaseSeeder> logger)
        {
            _dbContext = dbContext;
            _propertyDataStore = propertyDataStore;
            _agencyDataStore = agencyDataStore;
            _maintenanceDataStore = maintenanceDataStore;
            _settingsDataStore = settingsDataStore;
            _authDataStore = authDataStore;
            _logger = logger;
        }

        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Connecting to database using Entity Framework Core...");
                
                // Ensure database and all 7 modular schemas/27 tables are created
                await _dbContext.Database.EnsureCreatedAsync(cancellationToken);
                _logger.LogInformation("Database structure verified / created successfully.");

                // 1. Seed Auth Users
                if (!await _dbContext.Users.AnyAsync(cancellationToken))
                {
                    _logger.LogInformation("Seeding default Administrator user...");
                    var defaultAdmin = _authDataStore.GetUserByUsername("admin");
                    _dbContext.Users.Add(new UserDbEntity
                    {
                        Username = defaultAdmin?.Username ?? "admin",
                        Email = defaultAdmin?.Email ?? "admin@beeproperties.com",
                        PasswordHash = defaultAdmin?.PasswordHash ?? "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                        Salt = "default_salt",
                        FullName = defaultAdmin?.FullName ?? "Super Admin",
                        Role = "Super Admin",
                        Initials = "SA",
                        Phone = "+91 98765 43210",
                        Title = "Director of Operations",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    });
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                // 2. Seed Service Categories
                if (!await _dbContext.ServiceCategories.AnyAsync(cancellationToken))
                {
                    _logger.LogInformation("Seeding initial service categories...");
                    var categories = _settingsDataStore.GetServiceCategories();
                    foreach (var c in categories)
                    {
                        _dbContext.ServiceCategories.Add(new ServiceCategoryDbEntity
                        {
                            Name = c.Name,
                            Description = c.Description,
                            Status = c.Status,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                // 3. Seed Properties
                if (!await _dbContext.Properties.AnyAsync(cancellationToken))
                {
                    _logger.LogInformation("Seeding properties from initial data store...");
                    var properties = _propertyDataStore.GetAll();
                    foreach (var p in properties)
                    {
                        var propDb = new PropertyDbEntity
                        {
                            Code = p.Code,
                            Name = p.Name,
                            Tagline = p.Tagline,
                            PropertyType = p.PropertyType,
                            PropertyStatus = p.Status,
                            RegistrationNumber = p.RegistrationNumber,
                            BuiltYear = p.BuiltYear,
                            LandArea = p.LandArea,
                            Description = p.Description,
                            PrimaryImage = p.Image,
                            AddressLine1 = p.Address,
                            AddressLine2 = p.Address2,
                            City = p.City,
                            State = p.State,
                            PinCode = p.Zip,
                            Country = p.Country,
                            MapLink = p.MapLink,
                            Latitude = p.Latitude,
                            Longitude = p.Longitude,
                            PropertyManager = p.PropertyManager,
                            PropertyManagerContact = p.PropertyManagerContact,
                            EmergencyContact = p.EmergencyContact,
                            InternalNotes = p.InternalNotes,
                            Rating = (decimal)p.Rating,
                            AccentColor = p.AccentColor,
                            BadgeColor = p.BadgeColor,
                            ArchitecturalStyle = p.ArchitecturalStyle,
                            CreatedAt = DateTime.UtcNow
                        };

                        foreach (var b in p.Blocks)
                        {
                            var blockDb = new BlockDbEntity
                            {
                                Code = b.Code,
                                Name = b.Name,
                                Wing = b.Wing,
                                Elevators = b.Elevators,
                                PowerBackup = b.PowerBackup,
                                Security = b.Security,
                                Status = b.Status,
                                CreatedAt = DateTime.UtcNow
                            };

                            foreach (var f in b.Floors)
                            {
                                var floorDb = new FloorDbEntity
                                {
                                    FloorNumber = f.FloorNumber,
                                    Name = f.Name,
                                    CreatedAt = DateTime.UtcNow
                                };

                                foreach (var fl in f.Flats)
                                {
                                    floorDb.Flats.Add(new FlatDbEntity
                                    {
                                        FlatNumber = fl.Number,
                                        Status = fl.Status,
                                        BhkType = fl.BhkType,
                                        CarpetAreaSqFt = fl.CarpetAreaSqFt,
                                        CreatedAt = DateTime.UtcNow
                                    });
                                }

                                blockDb.Floors.Add(floorDb);
                            }

                            propDb.Blocks.Add(blockDb);
                        }

                        _dbContext.Properties.Add(propDb);
                    }
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                // 4. Seed Agencies
                if (!await _dbContext.Agencies.AnyAsync(cancellationToken))
                {
                    _logger.LogInformation("Seeding agencies from initial data store...");
                    var agencies = _agencyDataStore.GetAll();
                    foreach (var a in agencies)
                    {
                        var agencyDb = new AgencyDbEntity
                        {
                            AgencyCode = a.RegistrationRefNumber,
                            Name = a.Name,
                            PrimaryCategory = a.Category,
                            LogoText = a.LogoText,
                            ContactPerson = a.ContactPerson,
                            Phone = a.Phone,
                            Email = a.Email,
                            AgreementStatus = a.AgreementStatus,
                            AvailabilityStatus = a.AvailabilityStatus,
                            RegistrationRefNumber = a.RegistrationRefNumber,
                            RegistrationStatus = a.RegistrationStatus,
                            TermsAccepted = a.TermsAccepted,
                            DeclarationAccepted = a.DeclarationAccepted,
                            CreatedAt = DateTime.UtcNow
                        };

                        foreach (var pers in a.Personnel)
                        {
                            agencyDb.Personnel.Add(new AgencyPersonnelDbEntity
                            {
                                Name = pers.Name,
                                Role = pers.Role,
                                Experience = pers.Experience,
                                Phone = pers.Phone,
                                Email = pers.Email,
                                Status = pers.Status,
                                CurrentShift = pers.CurrentShift,
                                CreatedAt = DateTime.UtcNow
                            });
                        }

                        _dbContext.Agencies.Add(agencyDb);
                    }
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                _logger.LogInformation("Database initialized and synchronized with Entity Framework Core.");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Entity Framework Core database connection attempt resulted in an exception. System will continue operating in-memory.");
            }
        }
    }
}
