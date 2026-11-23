using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.repository.Commands.Interface;
using beeproperties.admin.webapi.repository.Entities;
using beeproperties.admin.webapi.repository.Query.Interface;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.services.Commands
{
    public class SecurityCommandService : ISecurityCommandService
    {
        private readonly ISecurityCommandRepository _commandRepository;
        private readonly ISecurityQueryRepository _queryRepository;
        private readonly IPropertyQueryRepository _propertyQueryRepository;
        private readonly IAgencyQueryRepository _agencyQueryRepository;

        public SecurityCommandService(
            ISecurityCommandRepository commandRepository,
            ISecurityQueryRepository queryRepository,
            IPropertyQueryRepository propertyQueryRepository,
            IAgencyQueryRepository agencyQueryRepository)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _propertyQueryRepository = propertyQueryRepository;
            _agencyQueryRepository = agencyQueryRepository;
        }

        public async Task<SecurityDeploymentDto> CreateDeploymentAsync(CreateSecurityDeploymentCommand command, CancellationToken cancellationToken = default)
        {
            var property = await _propertyQueryRepository.GetByIdAsync(command.ApartmentId, cancellationToken);
            var agency = await _agencyQueryRepository.GetByIdAsync(command.AgencyId, cancellationToken);

            var entity = new SecurityDeploymentEntity
            {
                Id = string.IsNullOrWhiteSpace(command.Id) ? $"sec-{DateTime.UtcNow.Ticks}" : command.Id.Trim(),
                PropertyId = command.ApartmentId,
                PropertyName = property?.Name ?? "Selected Apartment",
                AgencyId = command.AgencyId,
                AgencyName = agency?.Name ?? "Security Agency",
                BlockName = command.BlockName ?? "All Perimeter Gates & Main Entrance",
                Shift = command.Shift,
                RequiredGuards = command.RequiredGuards,
                AllocatedGuards = command.AllocatedGuards,
                PersonnelNames = command.PersonnelNames ?? new List<string>(),
                StartDate = command.StartDate ?? DateTime.UtcNow.ToString("yyyy-MM-dd"),
                EndDate = command.EndDate,
                Status = "Active"
            };

            var created = await _commandRepository.AddDeploymentAsync(entity, cancellationToken);
            return MapToDto(created);
        }

        public async Task<bool> UpdateStatusAsync(UpdateSecurityDeploymentStatusCommand command, CancellationToken cancellationToken = default)
        {
            var existing = await _queryRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existing == null) return false;

            existing.Status = command.Status;
            await _commandRepository.UpdateDeploymentAsync(existing, cancellationToken);
            return true;
        }

        public Task<bool> DeleteDeploymentAsync(DeleteSecurityDeploymentCommand command, CancellationToken cancellationToken = default)
        {
            return _commandRepository.DeleteDeploymentAsync(command.Id, cancellationToken);
        }

        public static SecurityDeploymentDto MapToDto(SecurityDeploymentEntity d)
        {
            return new SecurityDeploymentDto
            {
                Id = d.Id,
                ApartmentId = d.PropertyId,
                ApartmentName = d.PropertyName,
                AgencyId = d.AgencyId,
                AgencyName = d.AgencyName,
                BlockName = d.BlockName,
                Shift = d.Shift,
                RequiredGuards = d.RequiredGuards,
                AllocatedGuards = d.AllocatedGuards,
                PersonnelNames = d.PersonnelNames ?? new List<string>(),
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                Status = d.Status
            };
        }
    }
}
