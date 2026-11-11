using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.repository.Commands.Interface;
using beeproperties.admin.webapi.repository.Entities;
using beeproperties.admin.webapi.repository.Query.Interface;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.services.Commands
{
    public class CommunityCommandService : ICommunityCommandService
    {
        private readonly ICommunityCommandRepository _commandRepository;
        private readonly ICommunityQueryRepository _queryRepository;

        public CommunityCommandService(
            ICommunityCommandRepository commandRepository,
            ICommunityQueryRepository queryRepository)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
        }

        public async Task<CommunityDetailResponse> CreateCommunityAsync(CreateCommunityCommand command, CancellationToken cancellationToken = default)
        {
            var entity = new CommunityEntity
            {
                Id = string.IsNullOrWhiteSpace(command.Id) ? Guid.NewGuid().ToString("N") : command.Id.Trim(),
                Name = command.Name.Trim(),
                Tagline = command.Tagline.Trim(),
                Description = command.Description.Trim(),
                Address = command.Address.Trim(),
                City = command.City.Trim(),
                State = command.State.Trim(),
                Zip = command.Zip.Trim(),
                AerialImage = command.AerialImage.Trim(),
                TotalApartments = command.TotalApartments,
                TotalBlocks = command.TotalBlocks,
                TotalFloors = command.TotalFloors,
                TotalFlats = command.TotalFlats,
                Valuation = command.Valuation.Trim(),
                Established = command.Established,
                Amenities = command.Amenities.Select(a => new CommunityAmenityEntity
                {
                    Id = string.IsNullOrWhiteSpace(a.Id) ? Guid.NewGuid().ToString("N") : a.Id,
                    Name = a.Name,
                    Category = a.Category,
                    Desc = a.Desc,
                    Icon = a.Icon
                }).ToList(),
                Apartments = command.Apartments.Select(apt => new ApartmentTemplateEntity
                {
                    Id = string.IsNullOrWhiteSpace(apt.Id) ? Guid.NewGuid().ToString("N") : apt.Id,
                    Code = apt.Code,
                    Name = apt.Name,
                    Tagline = apt.Tagline,
                    Image = apt.Image,
                    Rating = apt.Rating,
                    BuiltYear = apt.BuiltYear,
                    AccentColor = apt.AccentColor,
                    BadgeColor = apt.BadgeColor,
                    ArchitecturalStyle = apt.ArchitecturalStyle,
                    Address = apt.Address,
                    Description = apt.Description,
                    Features = apt.Features ?? new List<string>(),
                    Blocks = apt.Blocks.Select(b => new ApartmentBlockEntity
                    {
                        Id = string.IsNullOrWhiteSpace(b.Id) ? Guid.NewGuid().ToString("N") : b.Id,
                        Code = b.Code,
                        Name = b.Name,
                        Wing = b.Wing,
                        Elevators = b.Elevators,
                        PowerBackup = b.PowerBackup,
                        Security = b.Security
                    }).ToList()
                }).ToList(),
                Flats = command.Flats.Select(f => new CommunityFlatEntity
                {
                    Id = string.IsNullOrWhiteSpace(f.Id) ? Guid.NewGuid().ToString("N") : f.Id,
                    FlatNumber = f.FlatNumber,
                    ApartmentId = f.ApartmentId,
                    ApartmentCode = f.ApartmentCode,
                    ApartmentName = f.ApartmentName,
                    ApartmentAccent = f.ApartmentAccent,
                    BlockId = f.BlockId,
                    BlockCode = f.BlockCode,
                    BlockName = f.BlockName,
                    BlockWing = f.BlockWing,
                    Floor = f.Floor,
                    Bhk = f.Bhk,
                    Bedrooms = f.Bedrooms,
                    Bathrooms = f.Bathrooms,
                    Sqft = f.Sqft,
                    Rent = f.Rent,
                    Deposit = f.Deposit,
                    TypeTag = f.TypeTag,
                    Facing = f.Facing,
                    Balcony = f.Balcony,
                    Status = f.Status,
                    StatusNote = f.StatusNote,
                    CompletionDate = f.CompletionDate,
                    ParkingSlot = f.ParkingSlot,
                    SmartLockStatus = f.SmartLockStatus,
                    Temperature = f.Temperature,
                    PowerBackup = f.PowerBackup,
                    WaterMeterReading = f.WaterMeterReading,
                    ElectricityMeterReading = f.ElectricityMeterReading,
                    Features = f.Features ?? new List<string>(),
                    Tenant = f.Tenant == null ? null : new TenantEntity
                    {
                        Name = f.Tenant.Name,
                        Avatar = f.Tenant.Avatar,
                        Email = f.Tenant.Email,
                        Phone = f.Tenant.Phone,
                        LeaseStart = f.Tenant.LeaseStart,
                        LeaseEnd = f.Tenant.LeaseEnd,
                        PaymentStatus = f.Tenant.PaymentStatus,
                        Occupants = f.Tenant.Occupants
                    }
                }).ToList()
            };

            var added = await _commandRepository.AddAsync(entity, cancellationToken);
            return MapToDetailResponse(added);
        }

        public async Task<CommunityDetailResponse?> UpdateCommunityAsync(UpdateCommunityCommand command, CancellationToken cancellationToken = default)
        {
            var existing = await _queryRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existing == null)
            {
                return null;
            }

            existing.Name = command.Name.Trim();
            existing.Tagline = command.Tagline.Trim();
            existing.Description = command.Description.Trim();
            existing.Address = command.Address.Trim();
            existing.City = command.City.Trim();
            existing.State = command.State.Trim();
            existing.Zip = command.Zip.Trim();
            existing.AerialImage = command.AerialImage.Trim();
            existing.TotalApartments = command.TotalApartments;
            existing.TotalBlocks = command.TotalBlocks;
            existing.TotalFloors = command.TotalFloors;
            existing.TotalFlats = command.TotalFlats;
            existing.Valuation = command.Valuation.Trim();
            existing.Established = command.Established;

            if (command.Amenities != null)
            {
                existing.Amenities = command.Amenities.Select(a => new CommunityAmenityEntity
                {
                    Id = string.IsNullOrWhiteSpace(a.Id) ? Guid.NewGuid().ToString("N") : a.Id,
                    Name = a.Name,
                    Category = a.Category,
                    Desc = a.Desc,
                    Icon = a.Icon
                }).ToList();
            }

            if (command.Apartments != null)
            {
                existing.Apartments = command.Apartments.Select(apt => new ApartmentTemplateEntity
                {
                    Id = string.IsNullOrWhiteSpace(apt.Id) ? Guid.NewGuid().ToString("N") : apt.Id,
                    Code = apt.Code,
                    Name = apt.Name,
                    Tagline = apt.Tagline,
                    Image = apt.Image,
                    Rating = apt.Rating,
                    BuiltYear = apt.BuiltYear,
                    AccentColor = apt.AccentColor,
                    BadgeColor = apt.BadgeColor,
                    ArchitecturalStyle = apt.ArchitecturalStyle,
                    Address = apt.Address,
                    Description = apt.Description,
                    Features = apt.Features ?? new List<string>(),
                    Blocks = apt.Blocks.Select(b => new ApartmentBlockEntity
                    {
                        Id = string.IsNullOrWhiteSpace(b.Id) ? Guid.NewGuid().ToString("N") : b.Id,
                        Code = b.Code,
                        Name = b.Name,
                        Wing = b.Wing,
                        Elevators = b.Elevators,
                        PowerBackup = b.PowerBackup,
                        Security = b.Security
                    }).ToList()
                }).ToList();
            }

            if (command.Flats != null)
            {
                existing.Flats = command.Flats.Select(f => new CommunityFlatEntity
                {
                    Id = string.IsNullOrWhiteSpace(f.Id) ? Guid.NewGuid().ToString("N") : f.Id,
                    FlatNumber = f.FlatNumber,
                    ApartmentId = f.ApartmentId,
                    ApartmentCode = f.ApartmentCode,
                    ApartmentName = f.ApartmentName,
                    ApartmentAccent = f.ApartmentAccent,
                    BlockId = f.BlockId,
                    BlockCode = f.BlockCode,
                    BlockName = f.BlockName,
                    BlockWing = f.BlockWing,
                    Floor = f.Floor,
                    Bhk = f.Bhk,
                    Bedrooms = f.Bedrooms,
                    Bathrooms = f.Bathrooms,
                    Sqft = f.Sqft,
                    Rent = f.Rent,
                    Deposit = f.Deposit,
                    TypeTag = f.TypeTag,
                    Facing = f.Facing,
                    Balcony = f.Balcony,
                    Status = f.Status,
                    StatusNote = f.StatusNote,
                    CompletionDate = f.CompletionDate,
                    ParkingSlot = f.ParkingSlot,
                    SmartLockStatus = f.SmartLockStatus,
                    Temperature = f.Temperature,
                    PowerBackup = f.PowerBackup,
                    WaterMeterReading = f.WaterMeterReading,
                    ElectricityMeterReading = f.ElectricityMeterReading,
                    Features = f.Features ?? new List<string>(),
                    Tenant = f.Tenant == null ? null : new TenantEntity
                    {
                        Name = f.Tenant.Name,
                        Avatar = f.Tenant.Avatar,
                        Email = f.Tenant.Email,
                        Phone = f.Tenant.Phone,
                        LeaseStart = f.Tenant.LeaseStart,
                        LeaseEnd = f.Tenant.LeaseEnd,
                        PaymentStatus = f.Tenant.PaymentStatus,
                        Occupants = f.Tenant.Occupants
                    }
                }).ToList();
            }

            var updated = await _commandRepository.UpdateAsync(existing, cancellationToken);
            return updated == null ? null : MapToDetailResponse(updated);
        }

        public async Task<bool> DeleteCommunityAsync(DeleteCommunityCommand command, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(command.Id))
            {
                return false;
            }

            return await _commandRepository.DeleteAsync(command.Id, cancellationToken);
        }

        private static CommunityDetailResponse MapToDetailResponse(CommunityEntity entity)
        {
            return new CommunityDetailResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Tagline = entity.Tagline,
                Description = entity.Description,
                Address = entity.Address,
                City = entity.City,
                State = entity.State,
                Zip = entity.Zip,
                AerialImage = entity.AerialImage,
                TotalApartments = entity.TotalApartments > 0 ? entity.TotalApartments : entity.Apartments.Count,
                TotalBlocks = entity.TotalBlocks > 0 ? entity.TotalBlocks : entity.Apartments.Sum(a => a.Blocks.Count),
                TotalFloors = entity.TotalFloors,
                TotalFlats = entity.TotalFlats > 0 ? entity.TotalFlats : entity.Flats.Count,
                Valuation = entity.Valuation,
                Established = entity.Established,
                TotalAmenities = entity.Amenities.Count,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Amenities = entity.Amenities.Select(a => new CommunityAmenityDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Category = a.Category,
                    Desc = a.Desc,
                    Icon = a.Icon
                }).ToList(),
                Apartments = entity.Apartments.Select(apt => new ApartmentTemplateDto
                {
                    Id = apt.Id,
                    Code = apt.Code,
                    Name = apt.Name,
                    Tagline = apt.Tagline,
                    Image = apt.Image,
                    Rating = apt.Rating,
                    BuiltYear = apt.BuiltYear,
                    AccentColor = apt.AccentColor,
                    BadgeColor = apt.BadgeColor,
                    ArchitecturalStyle = apt.ArchitecturalStyle,
                    Address = apt.Address,
                    Description = apt.Description,
                    Features = apt.Features,
                    Blocks = apt.Blocks.Select(b => new ApartmentBlockDto
                    {
                        Id = b.Id,
                        Code = b.Code,
                        Name = b.Name,
                        Wing = b.Wing,
                        Elevators = b.Elevators,
                        PowerBackup = b.PowerBackup,
                        Security = b.Security
                    }).ToList()
                }).ToList(),
                Flats = entity.Flats.Select(f => new CommunityFlatDto
                {
                    Id = f.Id,
                    FlatNumber = f.FlatNumber,
                    ApartmentId = f.ApartmentId,
                    ApartmentCode = f.ApartmentCode,
                    ApartmentName = f.ApartmentName,
                    ApartmentAccent = f.ApartmentAccent,
                    BlockId = f.BlockId,
                    BlockCode = f.BlockCode,
                    BlockName = f.BlockName,
                    BlockWing = f.BlockWing,
                    Floor = f.Floor,
                    Bhk = f.Bhk,
                    Bedrooms = f.Bedrooms,
                    Bathrooms = f.Bathrooms,
                    Sqft = f.Sqft,
                    Rent = f.Rent,
                    Deposit = f.Deposit,
                    TypeTag = f.TypeTag,
                    Facing = f.Facing,
                    Balcony = f.Balcony,
                    Status = f.Status,
                    StatusNote = f.StatusNote,
                    CompletionDate = f.CompletionDate,
                    ParkingSlot = f.ParkingSlot,
                    SmartLockStatus = f.SmartLockStatus,
                    Temperature = f.Temperature,
                    PowerBackup = f.PowerBackup,
                    WaterMeterReading = f.WaterMeterReading,
                    ElectricityMeterReading = f.ElectricityMeterReading,
                    Features = f.Features,
                    Tenant = f.Tenant == null ? null : new TenantDto
                    {
                        Name = f.Tenant.Name,
                        Avatar = f.Tenant.Avatar,
                        Email = f.Tenant.Email,
                        Phone = f.Tenant.Phone,
                        LeaseStart = f.Tenant.LeaseStart,
                        LeaseEnd = f.Tenant.LeaseEnd,
                        PaymentStatus = f.Tenant.PaymentStatus,
                        Occupants = f.Tenant.Occupants
                    }
                }).ToList()
            };
        }
    }
}

