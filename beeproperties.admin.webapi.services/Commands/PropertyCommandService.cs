using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.repository.Commands.Interface;
using beeproperties.admin.webapi.repository.Entities;
using beeproperties.admin.webapi.repository.Query.Interface;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.services.Commands
{
    public class PropertyCommandService : IPropertyCommandService
    {
        private readonly IPropertyCommandRepository _commandRepository;
        private readonly IPropertyQueryRepository _queryRepository;

        public PropertyCommandService(
            IPropertyCommandRepository commandRepository,
            IPropertyQueryRepository queryRepository)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
        }

        public async Task<PropertyDetailResponse> CreatePropertyAsync(CreatePropertyCommand command, CancellationToken cancellationToken = default)
        {
            var entity = new PropertyEntity
            {
                Id = string.IsNullOrWhiteSpace(command.Id) ? $"apt-{DateTime.UtcNow.Ticks}" : command.Id.Trim(),
                Code = command.Code ?? $"PROP-{DateTime.UtcNow:yyyyMMdd}",
                Name = command.Name.Trim(),
                Tagline = command.Tagline ?? string.Empty,
                PropertyType = command.PropertyType,
                Status = command.Status,
                Address = command.Address.Trim(),
                Address2 = command.Address2 ?? string.Empty,
                City = command.City.Trim(),
                State = command.State.Trim(),
                Zip = command.Zip.Trim(),
                Country = command.Country.Trim(),
                MapLink = command.MapLink ?? string.Empty,
                Latitude = command.Latitude,
                Longitude = command.Longitude,
                Image = command.Image ?? string.Empty,
                AdditionalImages = command.AdditionalImages ?? new List<string>(),
                BuiltYear = command.BuiltYear,
                RegistrationNumber = command.RegistrationNumber ?? string.Empty,
                LandArea = command.LandArea ?? string.Empty,
                Description = command.Description ?? string.Empty,
                PropertyManager = command.PropertyManager ?? string.Empty,
                PropertyManagerContact = command.PropertyManagerContact ?? string.Empty,
                EmergencyContact = command.EmergencyContact ?? string.Empty,
                InternalNotes = command.InternalNotes ?? string.Empty,
                RwaStatus = command.RwaStatus ?? "Not Registered",
                Blocks = (command.Blocks ?? new List<PropertyBlockDetailDto>()).Select(MapBlockDtoToEntity).ToList(),
                Documents = (command.Documents ?? new List<PropertyDocumentDto>()).Select(MapDocumentDtoToEntity).ToList()
            };

            var created = await _commandRepository.AddPropertyAsync(entity, cancellationToken);
            return MapToDetailResponse(created);
        }

        public async Task<PropertyDetailResponse?> UpdatePropertyAsync(UpdatePropertyCommand command, CancellationToken cancellationToken = default)
        {
            var existing = await _queryRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existing == null) return null;

            existing.Name = command.Name.Trim();
            if (command.Code != null) existing.Code = command.Code;
            if (command.Tagline != null) existing.Tagline = command.Tagline;
            existing.PropertyType = command.PropertyType;
            existing.Status = command.Status;
            existing.Address = command.Address.Trim();
            if (command.Address2 != null) existing.Address2 = command.Address2;
            existing.City = command.City.Trim();
            existing.State = command.State.Trim();
            existing.Zip = command.Zip.Trim();
            existing.Country = command.Country.Trim();
            if (command.MapLink != null) existing.MapLink = command.MapLink;
            if (command.Latitude.HasValue) existing.Latitude = command.Latitude;
            if (command.Longitude.HasValue) existing.Longitude = command.Longitude;
            if (!string.IsNullOrEmpty(command.Image)) existing.Image = command.Image;
            if (command.AdditionalImages != null) existing.AdditionalImages = command.AdditionalImages;
            existing.BuiltYear = command.BuiltYear;
            if (command.RegistrationNumber != null) existing.RegistrationNumber = command.RegistrationNumber;
            if (command.LandArea != null) existing.LandArea = command.LandArea;
            if (command.Description != null) existing.Description = command.Description;
            if (command.PropertyManager != null) existing.PropertyManager = command.PropertyManager;
            if (command.PropertyManagerContact != null) existing.PropertyManagerContact = command.PropertyManagerContact;
            if (command.EmergencyContact != null) existing.EmergencyContact = command.EmergencyContact;
            if (command.InternalNotes != null) existing.InternalNotes = command.InternalNotes;
            if (command.RwaStatus != null) existing.RwaStatus = command.RwaStatus;

            var updated = await _commandRepository.UpdatePropertyAsync(existing, cancellationToken);
            return updated == null ? null : MapToDetailResponse(updated);
        }

        public Task<bool> DeletePropertyAsync(DeletePropertyCommand command, CancellationToken cancellationToken = default)
        {
            return _commandRepository.DeletePropertyAsync(command.Id, cancellationToken);
        }

        public async Task<PropertyBlockDetailDto?> CreateBlockAsync(CreateBlockCommand command, CancellationToken cancellationToken = default)
        {
            var property = await _queryRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null) return null;

            var block = new PropertyBlockEntity
            {
                Id = $"blk-{DateTime.UtcNow.Ticks}",
                Code = $"BLK-{command.Name.Replace(" ", "").ToUpper()}",
                Name = command.Name.Trim(),
                Wing = command.Wing ?? string.Empty,
                FloorsCount = command.FloorsCount,
                FlatsCount = command.FloorsCount * command.FlatsPerFloor,
                OccupiedFlats = 0,
                VacantFlats = command.FloorsCount * command.FlatsPerFloor,
                Elevators = command.Elevators,
                PowerBackup = command.PowerBackup ?? "100% DG Backed",
                Security = command.Security ?? "Biometric FaceID + RFID",
                Status = command.Status ?? "Operational"
            };

            // Auto-generate floors & flats
            for (int f = 1; f <= command.FloorsCount; f++)
            {
                var floor = new PropertyFloorEntity
                {
                    Id = $"flr-{block.Id}-{f}",
                    FloorNumber = f,
                    Name = $"Floor {f}",
                    TotalFlats = command.FlatsPerFloor,
                    OccupiedFlats = 0,
                    VacantFlats = command.FlatsPerFloor
                };

                for (int fl = 1; fl <= command.FlatsPerFloor; fl++)
                {
                    floor.Flats.Add(new PropertyFlatEntity
                    {
                        Id = $"flat-{block.Id}-{f}{fl:D2}",
                        Number = $"{f}{fl:D2}",
                        Status = "Vacant",
                        BhkType = "3 BHK",
                        CarpetAreaSqFt = 1650
                    });
                }
                block.Floors.Add(floor);
            }

            property.Blocks.Add(block);
            await _commandRepository.UpdatePropertyAsync(property, cancellationToken);

            return MapBlockEntityToDto(block);
        }

        public async Task<PropertyBlockDetailDto?> UpdateBlockAsync(UpdateBlockCommand command, CancellationToken cancellationToken = default)
        {
            var property = await _queryRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null) return null;

            var block = property.Blocks.FirstOrDefault(b => b.Id == command.BlockId);
            if (block == null) return null;

            block.Name = command.Name.Trim();
            if (command.Wing != null) block.Wing = command.Wing;
            block.Elevators = command.Elevators;
            if (command.PowerBackup != null) block.PowerBackup = command.PowerBackup;
            if (command.Security != null) block.Security = command.Security;
            if (command.Status != null) block.Status = command.Status;

            await _commandRepository.UpdatePropertyAsync(property, cancellationToken);
            return MapBlockEntityToDto(block);
        }

        public async Task<bool> DeleteBlockAsync(DeleteBlockCommand command, CancellationToken cancellationToken = default)
        {
            var property = await _queryRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null) return false;

            var removed = property.Blocks.RemoveAll(b => b.Id == command.BlockId) > 0;
            if (removed)
            {
                await _commandRepository.UpdatePropertyAsync(property, cancellationToken);
            }
            return removed;
        }

        public async Task<FloorDto?> CreateFloorAsync(CreateFloorCommand command, CancellationToken cancellationToken = default)
        {
            var property = await _queryRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null) return null;

            var block = property.Blocks.FirstOrDefault(b => b.Id == command.BlockId);
            if (block == null) return null;

            var floorNumber = command.FloorNumber ?? (block.Floors.Count + 1);
            var floor = new PropertyFloorEntity
            {
                Id = $"flr-{block.Id}-{floorNumber}",
                FloorNumber = floorNumber,
                Name = command.Name ?? $"Floor {floorNumber}",
                TotalFlats = command.FlatsCount,
                OccupiedFlats = 0,
                VacantFlats = command.FlatsCount
            };

            for (int fl = 1; fl <= command.FlatsCount; fl++)
            {
                floor.Flats.Add(new PropertyFlatEntity
                {
                    Id = $"flat-{block.Id}-{floorNumber}{fl:D2}",
                    Number = $"{floorNumber}{fl:D2}",
                    Status = "Vacant",
                    BhkType = "3 BHK",
                    CarpetAreaSqFt = 1650
                });
            }

            block.Floors.Add(floor);
            block.FloorsCount = block.Floors.Count;
            block.FlatsCount = block.Floors.Sum(f => f.TotalFlats);
            block.VacantFlats = block.Floors.Sum(f => f.VacantFlats);

            await _commandRepository.UpdatePropertyAsync(property, cancellationToken);
            return MapFloorEntityToDto(floor);
        }

        public async Task<FloorDto?> UpdateFloorAsync(UpdateFloorCommand command, CancellationToken cancellationToken = default)
        {
            var property = await _queryRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null) return null;

            var block = property.Blocks.FirstOrDefault(b => b.Id == command.BlockId);
            if (block == null) return null;

            var floor = block.Floors.FirstOrDefault(f => f.Id == command.FloorId);
            if (floor == null) return null;

            floor.Name = command.Name.Trim();
            await _commandRepository.UpdatePropertyAsync(property, cancellationToken);
            return MapFloorEntityToDto(floor);
        }

        public async Task<bool> DeleteFloorAsync(DeleteFloorCommand command, CancellationToken cancellationToken = default)
        {
            var property = await _queryRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null) return false;

            var block = property.Blocks.FirstOrDefault(b => b.Id == command.BlockId);
            if (block == null) return false;

            var removed = block.Floors.RemoveAll(f => f.Id == command.FloorId) > 0;
            if (removed)
            {
                block.FloorsCount = block.Floors.Count;
                block.FlatsCount = block.Floors.Sum(f => f.TotalFlats);
                block.OccupiedFlats = block.Floors.Sum(f => f.OccupiedFlats);
                block.VacantFlats = block.Floors.Sum(f => f.VacantFlats);
                await _commandRepository.UpdatePropertyAsync(property, cancellationToken);
            }
            return removed;
        }

        public async Task<FlatDto?> CreateFlatAsync(CreateFlatCommand command, CancellationToken cancellationToken = default)
        {
            var property = await _queryRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null) return null;

            var block = property.Blocks.FirstOrDefault(b => b.Id == command.BlockId);
            if (block == null) return null;

            var floor = block.Floors.FirstOrDefault(f => f.Id == command.FloorId);
            if (floor == null) return null;

            var flat = new PropertyFlatEntity
            {
                Id = $"flat-{block.Id}-{command.Number.Trim()}",
                Number = command.Number.Trim(),
                Status = command.Status,
                BhkType = command.BhkType ?? "3 BHK",
                CarpetAreaSqFt = command.CarpetAreaSqFt ?? 1650,
                ResidentType = command.ResidentType,
                ResidentName = command.ResidentName,
                Phone = command.Phone,
                Email = command.Email
            };

            floor.Flats.Add(flat);
            floor.TotalFlats = floor.Flats.Count;
            floor.OccupiedFlats = floor.Flats.Count(fl => fl.Status == "Occupied");
            floor.VacantFlats = floor.Flats.Count(fl => fl.Status != "Occupied");

            block.FlatsCount = block.Floors.Sum(f => f.TotalFlats);
            block.OccupiedFlats = block.Floors.Sum(f => f.OccupiedFlats);
            block.VacantFlats = block.Floors.Sum(f => f.VacantFlats);

            await _commandRepository.UpdatePropertyAsync(property, cancellationToken);
            return MapFlatEntityToDto(flat);
        }

        public async Task<FlatDto?> UpdateFlatResidentAsync(UpdateFlatResidentCommand command, CancellationToken cancellationToken = default)
        {
            var property = await _queryRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null) return null;

            var block = property.Blocks.FirstOrDefault(b => b.Id == command.BlockId);
            if (block == null) return null;

            var floor = block.Floors.FirstOrDefault(f => f.Id == command.FloorId);
            if (floor == null) return null;

            var flat = floor.Flats.FirstOrDefault(fl => fl.Number.Equals(command.FlatNumber, StringComparison.OrdinalIgnoreCase));
            if (flat == null) return null;

            flat.Status = command.Status;
            flat.ResidentType = command.ResidentType;
            flat.ResidentName = command.ResidentName;
            flat.Phone = command.Phone;
            flat.Email = command.Email;

            floor.OccupiedFlats = floor.Flats.Count(fl => fl.Status == "Occupied");
            floor.VacantFlats = floor.Flats.Count(fl => fl.Status != "Occupied");
            block.OccupiedFlats = block.Floors.Sum(f => f.OccupiedFlats);
            block.VacantFlats = block.Floors.Sum(f => f.VacantFlats);

            await _commandRepository.UpdatePropertyAsync(property, cancellationToken);
            return MapFlatEntityToDto(flat);
        }

        public async Task<bool> DeleteFlatAsync(DeleteFlatCommand command, CancellationToken cancellationToken = default)
        {
            var property = await _queryRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null) return false;

            var block = property.Blocks.FirstOrDefault(b => b.Id == command.BlockId);
            if (block == null) return false;

            var floor = block.Floors.FirstOrDefault(f => f.Id == command.FloorId);
            if (floor == null) return false;

            var removed = floor.Flats.RemoveAll(fl => fl.Number.Equals(command.FlatNumber, StringComparison.OrdinalIgnoreCase)) > 0;
            if (removed)
            {
                floor.TotalFlats = floor.Flats.Count;
                floor.OccupiedFlats = floor.Flats.Count(fl => fl.Status == "Occupied");
                floor.VacantFlats = floor.Flats.Count(fl => fl.Status != "Occupied");
                block.FlatsCount = block.Floors.Sum(f => f.TotalFlats);
                block.OccupiedFlats = block.Floors.Sum(f => f.OccupiedFlats);
                block.VacantFlats = block.Floors.Sum(f => f.VacantFlats);
                await _commandRepository.UpdatePropertyAsync(property, cancellationToken);
            }
            return removed;
        }

        public async Task<PropertyDocumentDto?> AddDocumentAsync(AddPropertyDocumentCommand command, CancellationToken cancellationToken = default)
        {
            var property = await _queryRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null) return null;

            var doc = new PropertyDocumentEntity
            {
                Id = $"pdoc-{DateTime.UtcNow.Ticks}",
                Name = command.Name.Trim(),
                Category = command.Category.Trim(),
                FileUrl = command.FileUrl.Trim(),
                FileType = command.FileType,
                FileSize = command.FileSize,
                FileSizeBytes = command.FileSizeBytes,
                Description = command.Description ?? string.Empty,
                UploadDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
            };

            property.Documents.Add(doc);
            await _commandRepository.UpdatePropertyAsync(property, cancellationToken);

            return MapDocumentEntityToDto(doc);
        }

        public async Task<bool> DeleteDocumentAsync(DeletePropertyDocumentCommand command, CancellationToken cancellationToken = default)
        {
            var property = await _queryRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null) return false;

            var removed = property.Documents.RemoveAll(d => d.Id == command.DocumentId) > 0;
            if (removed)
            {
                await _commandRepository.UpdatePropertyAsync(property, cancellationToken);
            }
            return removed;
        }

        public async Task<RwaDetailResponseDto?> SaveRwaAsync(SaveRwaDetailsCommand command, CancellationToken cancellationToken = default)
        {
            var property = await _queryRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null) return null;

            property.RwaStatus = command.Status;
            property.Rwa = new PropertyRwaEntity
            {
                Status = command.Status,
                Name = command.Name.Trim(),
                RegNumber = command.RegNumber ?? string.Empty,
                RegDate = command.RegDate ?? string.Empty,
                President = command.President ?? string.Empty,
                Secretary = command.Secretary ?? string.Empty,
                Treasurer = command.Treasurer ?? string.Empty,
                ContactPhone = command.ContactPhone ?? string.Empty,
                ContactEmail = command.ContactEmail ?? string.Empty,
                OfficeHours = command.OfficeHours ?? string.Empty,
                Username = command.Username ?? string.Empty,
                PasswordHash = command.Password ?? string.Empty,
                TotalMembers = command.Committee?.Count ?? 0,
                Committee = (command.Committee ?? new List<RwaCommitteeMemberDto>()).Select(c => new PropertyRwaMemberEntity
                {
                    Id = string.IsNullOrWhiteSpace(c.Id) ? $"comm-{DateTime.UtcNow.Ticks}-{Guid.NewGuid():N}" : c.Id,
                    Name = c.Name,
                    Role = c.Role,
                    Flat = c.Flat,
                    Phone = c.Phone,
                    Email = c.Email ?? string.Empty
                }).ToList()
            };

            await _commandRepository.UpdatePropertyAsync(property, cancellationToken);
            return MapRwaEntityToDto(property.Rwa);
        }

        public async Task<bool> UpdateRwaStatusAsync(UpdateRwaStatusCommand command, CancellationToken cancellationToken = default)
        {
            var property = await _queryRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null) return false;

            property.RwaStatus = command.Status;
            if (property.Rwa != null)
            {
                property.Rwa.Status = command.Status;
            }
            await _commandRepository.UpdatePropertyAsync(property, cancellationToken);
            return true;
        }

        public async Task<RwaCommitteeMemberDto?> AddRwaCommitteeMemberAsync(AddRwaCommitteeMemberCommand command, CancellationToken cancellationToken = default)
        {
            var property = await _queryRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null) return null;

            if (property.Rwa == null)
            {
                property.Rwa = new PropertyRwaEntity
                {
                    Name = $"{property.Name} RWA Association",
                    Status = "Active"
                };
            }

            var member = new PropertyRwaMemberEntity
            {
                Id = $"comm-{DateTime.UtcNow.Ticks}",
                Name = command.Name.Trim(),
                Role = command.Role.Trim(),
                Flat = command.Flat.Trim(),
                Phone = command.Phone.Trim(),
                Email = command.Email ?? string.Empty
            };

            property.Rwa.Committee.Add(member);
            property.Rwa.TotalMembers = property.Rwa.Committee.Count;
            await _commandRepository.UpdatePropertyAsync(property, cancellationToken);

            return new RwaCommitteeMemberDto
            {
                Id = member.Id,
                Name = member.Name,
                Role = member.Role,
                Flat = member.Flat,
                Phone = member.Phone,
                Email = member.Email
            };
        }

        public async Task<bool> DeleteRwaCommitteeMemberAsync(DeleteRwaCommitteeMemberCommand command, CancellationToken cancellationToken = default)
        {
            var property = await _queryRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property?.Rwa == null) return false;

            var removed = property.Rwa.Committee.RemoveAll(c => c.Id == command.MemberId) > 0;
            if (removed)
            {
                await _commandRepository.UpdatePropertyAsync(property, cancellationToken);
            }
            return removed;
        }

        public static PropertyDetailResponse MapToDetailResponse(PropertyEntity p)
        {
            return new PropertyDetailResponse
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
                TotalFlats = p.Blocks.Sum(b => b.FlatsCount),
                OccupiedFlats = p.Blocks.Sum(b => b.OccupiedFlats),
                VacantFlats = p.Blocks.Sum(b => b.VacantFlats),
                ActiveMaintenance = p.Blocks.Sum(b => b.ActiveMaintenance),
                RwaStatus = p.RwaStatus,
                LandArea = p.LandArea,
                Description = p.Description,
                RegistrationNumber = p.RegistrationNumber,
                PropertyManager = p.PropertyManager,
                PropertyManagerContact = p.PropertyManagerContact,
                EmergencyContact = p.EmergencyContact,
                InternalNotes = p.InternalNotes,
                MapLink = p.MapLink,
                Rating = p.Rating,
                AccentColor = p.AccentColor,
                BadgeColor = p.BadgeColor,
                ArchitecturalStyle = p.ArchitecturalStyle,
                AdditionalImages = p.AdditionalImages ?? new List<string>(),
                Features = p.Features ?? new List<string>(),
                Blocks = p.Blocks.Select(MapBlockEntityToDto).ToList(),
                Documents = p.Documents.Select(MapDocumentEntityToDto).ToList(),
                Rwa = p.Rwa == null ? null : MapRwaEntityToDto(p.Rwa)
            };
        }

        public static PropertyBlockDetailDto MapBlockEntityToDto(PropertyBlockEntity b)
        {
            return new PropertyBlockDetailDto
            {
                Id = b.Id,
                Code = b.Code,
                Name = b.Name,
                Wing = b.Wing,
                Elevators = b.Elevators,
                PowerBackup = b.PowerBackup,
                Security = b.Security,
                FloorsCount = b.FloorsCount,
                FlatsCount = b.FlatsCount,
                OccupiedFlats = b.OccupiedFlats,
                VacantFlats = b.VacantFlats,
                ActiveMaintenance = b.ActiveMaintenance,
                Status = b.Status,
                AssignedAgencies = b.AssignedAgencies ?? new List<string>(),
                Floors = b.Floors.Select(MapFloorEntityToDto).ToList()
            };
        }

        public static FloorDto MapFloorEntityToDto(PropertyFloorEntity f)
        {
            return new FloorDto
            {
                Id = f.Id,
                FloorNumber = f.FloorNumber,
                Name = f.Name,
                TotalFlats = f.TotalFlats,
                OccupiedFlats = f.OccupiedFlats,
                VacantFlats = f.VacantFlats,
                ActiveMaintenance = f.ActiveMaintenance,
                Flats = f.Flats.Select(MapFlatEntityToDto).ToList()
            };
        }

        public static FlatDto MapFlatEntityToDto(PropertyFlatEntity fl)
        {
            return new FlatDto
            {
                Id = fl.Id,
                Number = fl.Number,
                Status = fl.Status,
                ResidentType = fl.ResidentType,
                ResidentName = fl.ResidentName,
                Phone = fl.Phone,
                Email = fl.Email,
                BhkType = fl.BhkType,
                CarpetAreaSqFt = fl.CarpetAreaSqFt,
                ActiveMaintenance = fl.ActiveMaintenance
            };
        }

        public static PropertyDocumentDto MapDocumentEntityToDto(PropertyDocumentEntity d)
        {
            return new PropertyDocumentDto
            {
                Id = d.Id,
                PropertyId = d.PropertyId,
                Name = d.Name,
                Category = d.Category,
                FileUrl = d.FileUrl,
                FileType = d.FileType,
                FileSize = d.FileSize,
                FileSizeBytes = d.FileSizeBytes,
                Description = d.Description,
                UploadDate = d.UploadDate
            };
        }

        public static RwaDetailResponseDto MapRwaEntityToDto(PropertyRwaEntity r)
        {
            return new RwaDetailResponseDto
            {
                Id = "rwa-01",
                Status = r.Status,
                Name = r.Name,
                RegNumber = r.RegNumber,
                RegDate = r.RegDate,
                President = r.President,
                Secretary = r.Secretary,
                Treasurer = r.Treasurer,
                ContactPhone = r.ContactPhone,
                ContactEmail = r.ContactEmail,
                OfficeHours = r.OfficeHours,
                Username = r.Username ?? string.Empty,
                Password = r.PasswordHash ?? string.Empty,
                Committee = r.Committee.Select(c => new RwaCommitteeMemberDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Role = c.Role,
                    Flat = c.Flat,
                    Phone = c.Phone,
                    Email = c.Email
                }).ToList()
            };
        }

        private static PropertyBlockEntity MapBlockDtoToEntity(PropertyBlockDetailDto b)
        {
            return new PropertyBlockEntity
            {
                Id = string.IsNullOrWhiteSpace(b.Id) ? $"blk-{DateTime.UtcNow.Ticks}" : b.Id,
                Code = b.Code,
                Name = b.Name,
                Wing = b.Wing,
                Elevators = b.Elevators,
                PowerBackup = b.PowerBackup,
                Security = b.Security,
                FloorsCount = b.FloorsCount,
                FlatsCount = b.FlatsCount,
                OccupiedFlats = b.OccupiedFlats,
                VacantFlats = b.VacantFlats,
                ActiveMaintenance = b.ActiveMaintenance,
                Status = b.Status,
                AssignedAgencies = b.AssignedAgencies ?? new List<string>(),
                Floors = (b.Floors ?? new List<FloorDto>()).Select(f => new PropertyFloorEntity
                {
                    Id = string.IsNullOrWhiteSpace(f.Id) ? $"flr-{DateTime.UtcNow.Ticks}" : f.Id,
                    FloorNumber = f.FloorNumber,
                    Name = f.Name,
                    TotalFlats = f.TotalFlats,
                    OccupiedFlats = f.OccupiedFlats,
                    VacantFlats = f.VacantFlats,
                    ActiveMaintenance = f.ActiveMaintenance,
                    Flats = (f.Flats ?? new List<FlatDto>()).Select(fl => new PropertyFlatEntity
                    {
                        Id = string.IsNullOrWhiteSpace(fl.Id) ? $"flat-{DateTime.UtcNow.Ticks}" : fl.Id,
                        Number = fl.Number,
                        Status = fl.Status,
                        ResidentType = fl.ResidentType,
                        ResidentName = fl.ResidentName,
                        Phone = fl.Phone,
                        Email = fl.Email,
                        BhkType = fl.BhkType,
                        CarpetAreaSqFt = fl.CarpetAreaSqFt,
                        ActiveMaintenance = fl.ActiveMaintenance
                    }).ToList()
                }).ToList()
            };
        }

        private static PropertyDocumentEntity MapDocumentDtoToEntity(PropertyDocumentDto d)
        {
            return new PropertyDocumentEntity
            {
                Id = string.IsNullOrWhiteSpace(d.Id) ? $"doc-{DateTime.UtcNow.Ticks}" : d.Id,
                PropertyId = d.PropertyId,
                Name = d.Name,
                Category = d.Category,
                FileUrl = d.FileUrl,
                FileType = d.FileType,
                FileSize = d.FileSize,
                FileSizeBytes = d.FileSizeBytes,
                Description = d.Description,
                UploadDate = d.UploadDate
            };
        }
    }
}
