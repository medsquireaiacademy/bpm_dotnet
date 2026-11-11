using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.repository.Entities;
using beeproperties.admin.webapi.repository.Query.Interface;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.services.Query
{
    public class CommunityQueryService : ICommunityQueryService
    {
        private readonly ICommunityQueryRepository _queryRepository;

        public CommunityQueryService(ICommunityQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<List<CommunityResponse>> GetAllCommunitiesAsync(GetAllCommunitiesQuery query, CancellationToken cancellationToken = default)
        {
            var entities = await _queryRepository.GetAllAsync(cancellationToken);

            var filtered = entities.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                var term = query.SearchTerm.Trim().ToLowerInvariant();
                filtered = filtered.Where(c =>
                    c.Name.ToLowerInvariant().Contains(term) ||
                    c.Tagline.ToLowerInvariant().Contains(term) ||
                    c.City.ToLowerInvariant().Contains(term) ||
                    c.Address.ToLowerInvariant().Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(query.City))
            {
                filtered = filtered.Where(c => c.City.Equals(query.City.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(query.State))
            {
                filtered = filtered.Where(c => c.State.Equals(query.State.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            return filtered.Select(MapToResponse).ToList();
        }

        public async Task<CommunityDetailResponse?> GetCommunityByIdAsync(GetCommunityByIdQuery query, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(query.Id))
            {
                return null;
            }

            var entity = await _queryRepository.GetByIdAsync(query.Id, cancellationToken);
            return entity == null ? null : MapToDetailResponse(entity);
        }

        public Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
        {
            return _queryRepository.ExistsAsync(id, cancellationToken);
        }

        private static CommunityResponse MapToResponse(CommunityEntity entity)
        {
            return new CommunityResponse
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
                UpdatedAt = entity.UpdatedAt
            };
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

