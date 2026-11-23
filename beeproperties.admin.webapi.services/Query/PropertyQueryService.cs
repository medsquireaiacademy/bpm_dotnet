using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.repository.Entities;
using beeproperties.admin.webapi.repository.Query.Interface;
using beeproperties.admin.webapi.services.Commands;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.services.Query
{
    public class PropertyQueryService : IPropertyQueryService
    {
        private readonly IPropertyQueryRepository _queryRepository;

        public PropertyQueryService(IPropertyQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<List<PropertyResponse>> GetAllPropertiesAsync(GetAllPropertiesQuery query, CancellationToken cancellationToken = default)
        {
            var entities = await _queryRepository.GetAllAsync(cancellationToken);
            var filtered = entities.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                var term = query.SearchTerm.Trim().ToLowerInvariant();
                filtered = filtered.Where(p =>
                    p.Name.ToLowerInvariant().Contains(term) ||
                    p.Code.ToLowerInvariant().Contains(term) ||
                    p.City.ToLowerInvariant().Contains(term) ||
                    p.Address.ToLowerInvariant().Contains(term) ||
                    p.PropertyManager.ToLowerInvariant().Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(query.Status))
            {
                filtered = filtered.Where(p => p.Status.Contains(query.Status.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(query.City))
            {
                filtered = filtered.Where(p => p.City.Equals(query.City.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(query.State))
            {
                filtered = filtered.Where(p => p.State.Equals(query.State.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(query.PropertyType))
            {
                filtered = filtered.Where(p => p.PropertyType.Equals(query.PropertyType.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            return filtered.Select(MapToResponse).ToList();
        }

        public async Task<PropertyDetailResponse?> GetPropertyByIdAsync(GetPropertyByIdQuery query, CancellationToken cancellationToken = default)
        {
            var entity = await _queryRepository.GetByIdAsync(query.Id, cancellationToken);
            return entity == null ? null : PropertyCommandService.MapToDetailResponse(entity);
        }

        public async Task<PropertySummaryDto> GetPropertySummaryAsync(GetPropertySummaryQuery query, CancellationToken cancellationToken = default)
        {
            var entities = await _queryRepository.GetAllAsync(cancellationToken);
            if (!string.IsNullOrWhiteSpace(query.PropertyId))
            {
                entities = entities.Where(p => p.Id.Equals(query.PropertyId, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var totalProps = entities.Count;
            var totalBlocks = entities.Sum(p => p.Blocks.Count);
            var totalFloors = entities.Sum(p => p.Blocks.Sum(b => b.FloorsCount));
            var totalFlats = entities.Sum(p => p.Blocks.Sum(b => b.FlatsCount));
            var occupiedFlats = entities.Sum(p => p.Blocks.Sum(b => b.OccupiedFlats));
            var vacantFlats = entities.Sum(p => p.Blocks.Sum(b => b.VacantFlats));
            var activeMaint = entities.Sum(p => p.Blocks.Sum(b => b.ActiveMaintenance));

            return new PropertySummaryDto
            {
                TotalProperties = totalProps,
                TotalBlocks = totalBlocks,
                TotalFloors = totalFloors,
                TotalFlats = totalFlats,
                OccupiedFlats = occupiedFlats,
                VacantFlats = vacantFlats,
                OccupancyRate = totalFlats > 0 ? Math.Round(((double)occupiedFlats / totalFlats) * 100, 1) : 0,
                ActiveMaintenanceRequests = activeMaint
            };
        }

        public async Task<List<PropertyBlockDetailDto>> GetPropertyBlocksAsync(GetPropertyBlocksQuery query, CancellationToken cancellationToken = default)
        {
            var entity = await _queryRepository.GetByIdAsync(query.PropertyId, cancellationToken);
            if (entity == null) return new List<PropertyBlockDetailDto>();

            return entity.Blocks.Select(PropertyCommandService.MapBlockEntityToDto).ToList();
        }

        public async Task<List<FloorDto>> GetBlockFloorsAsync(GetBlockFloorsQuery query, CancellationToken cancellationToken = default)
        {
            var entity = await _queryRepository.GetByIdAsync(query.PropertyId, cancellationToken);
            if (entity == null) return new List<FloorDto>();

            var block = entity.Blocks.FirstOrDefault(b => b.Id.Equals(query.BlockId, StringComparison.OrdinalIgnoreCase));
            if (block == null) return new List<FloorDto>();

            return block.Floors.Select(PropertyCommandService.MapFloorEntityToDto).ToList();
        }

        public async Task<List<FlatDto>> GetFloorFlatsAsync(GetFloorFlatsQuery query, CancellationToken cancellationToken = default)
        {
            var entity = await _queryRepository.GetByIdAsync(query.PropertyId, cancellationToken);
            if (entity == null) return new List<FlatDto>();

            var block = entity.Blocks.FirstOrDefault(b => b.Id.Equals(query.BlockId, StringComparison.OrdinalIgnoreCase));
            if (block == null) return new List<FlatDto>();

            var floor = block.Floors.FirstOrDefault(f => f.Id.Equals(query.FloorId, StringComparison.OrdinalIgnoreCase));
            if (floor == null) return new List<FlatDto>();

            return floor.Flats.Select(PropertyCommandService.MapFlatEntityToDto).ToList();
        }

        public async Task<FlatDto?> GetFlatByNumberAsync(GetFlatByNumberQuery query, CancellationToken cancellationToken = default)
        {
            var entity = await _queryRepository.GetByIdAsync(query.PropertyId, cancellationToken);
            if (entity == null) return null;

            foreach (var b in entity.Blocks)
            {
                foreach (var f in b.Floors)
                {
                    var flat = f.Flats.FirstOrDefault(fl => fl.Number.Equals(query.FlatNumber, StringComparison.OrdinalIgnoreCase));
                    if (flat != null)
                    {
                        return PropertyCommandService.MapFlatEntityToDto(flat);
                    }
                }
            }

            return null;
        }

        public async Task<List<PropertyDocumentDto>> GetPropertyDocumentsAsync(GetPropertyDocumentsQuery query, CancellationToken cancellationToken = default)
        {
            var entity = await _queryRepository.GetByIdAsync(query.PropertyId, cancellationToken);
            if (entity == null) return new List<PropertyDocumentDto>();

            var docs = entity.Documents.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                docs = docs.Where(d => d.Category.Equals(query.Category, StringComparison.OrdinalIgnoreCase));
            }

            return docs.Select(PropertyCommandService.MapDocumentEntityToDto).ToList();
        }

        public async Task<RwaDetailResponseDto?> GetPropertyRwaAsync(GetPropertyRwaQuery query, CancellationToken cancellationToken = default)
        {
            var entity = await _queryRepository.GetByIdAsync(query.PropertyId, cancellationToken);
            if (entity?.Rwa == null) return null;

            return PropertyCommandService.MapRwaEntityToDto(entity.Rwa);
        }

        public Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
        {
            return _queryRepository.ExistsAsync(id, cancellationToken);
        }

        private static PropertyResponse MapToResponse(PropertyEntity p)
        {
            var totalFlats = p.Blocks.Sum(b => b.FlatsCount);
            var occupiedFlats = p.Blocks.Sum(b => b.OccupiedFlats);
            return new PropertyResponse
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                Tagline = p.Tagline,
                PropertyType = p.PropertyType,
                Status = p.Status,
                Address = p.Address,
                Address2 = p.Address2,
                City = p.City,
                State = p.State,
                Zip = p.Zip,
                Country = p.Country,
                Image = p.Image,
                BuiltYear = p.BuiltYear,
                TotalBlocks = p.Blocks.Count,
                TotalFloors = p.Blocks.Sum(b => b.FloorsCount),
                TotalFlats = totalFlats,
                OccupiedFlats = occupiedFlats,
                VacantFlats = p.Blocks.Sum(b => b.VacantFlats),
                ActiveMaintenance = p.Blocks.Sum(b => b.ActiveMaintenance),
                RwaStatus = p.RwaStatus
            };
        }
    }
}
