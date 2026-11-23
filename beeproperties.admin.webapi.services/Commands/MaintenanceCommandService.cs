using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.repository.Commands.Interface;
using beeproperties.admin.webapi.repository.Entities;
using beeproperties.admin.webapi.repository.Query.Interface;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.services.Commands
{
    public class MaintenanceCommandService : IMaintenanceCommandService
    {
        private readonly IMaintenanceCommandRepository _commandRepository;
        private readonly IMaintenanceQueryRepository _queryRepository;
        private readonly IAgencyQueryRepository _agencyQueryRepository;

        public MaintenanceCommandService(
            IMaintenanceCommandRepository commandRepository,
            IMaintenanceQueryRepository queryRepository,
            IAgencyQueryRepository agencyQueryRepository)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _agencyQueryRepository = agencyQueryRepository;
        }

        public async Task<MaintenanceTicketDetailDto> CreateTicketAsync(CreateMaintenanceTicketCommand command, CancellationToken cancellationToken = default)
        {
            var entity = new MaintenanceRequestEntity
            {
                Id = string.IsNullOrWhiteSpace(command.Id) ? $"maint-{DateTime.UtcNow.Ticks}" : command.Id.Trim(),
                TicketNumber = $"MNT-{DateTime.UtcNow:yyyyMMdd}-{DateTime.UtcNow.Ticks % 10000:D4}",
                Title = command.Title.Trim(),
                Description = command.Description.Trim(),
                Category = command.Category.Trim(),
                Priority = command.Priority ?? "Medium",
                Status = "Reported",
                ReportedDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm"),
                PropertyId = command.ApartmentId,
                PropertyName = command.ApartmentName,
                BlockId = command.BlockId,
                BlockName = command.BlockName,
                FloorId = command.FloorId,
                FlatNumber = command.FlatNumber,
                LocationDescription = command.LocationDescription
            };

            var created = await _commandRepository.AddRequestAsync(entity, cancellationToken);
            return MapToDetailDto(created);
        }

        public async Task<MaintenanceTicketDetailDto?> UpdateTicketAsync(UpdateMaintenanceTicketCommand command, CancellationToken cancellationToken = default)
        {
            var existing = await _queryRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existing == null) return null;

            existing.Title = command.Title.Trim();
            existing.Description = command.Description.Trim();
            existing.Category = command.Category.Trim();
            existing.Priority = command.Priority;

            var updated = await _commandRepository.UpdateRequestAsync(existing, cancellationToken);
            return updated == null ? null : MapToDetailDto(updated);
        }

        public async Task<bool> UpdateStatusAsync(UpdateMaintenanceStatusCommand command, CancellationToken cancellationToken = default)
        {
            var existing = await _queryRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existing == null) return false;

            existing.Status = command.Status;
            if (command.Status.Equals("Completed", StringComparison.OrdinalIgnoreCase))
            {
                existing.CompletedDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm");
            }

            await _commandRepository.UpdateRequestAsync(existing, cancellationToken);
            return true;
        }

        public async Task<MaintenanceTicketDetailDto?> AllocateAgencyAsync(AllocateAgencyToMaintenanceCommand command, CancellationToken cancellationToken = default)
        {
            var existing = await _queryRepository.GetByIdAsync(command.MaintenanceId, cancellationToken);
            if (existing == null) return null;

            var agency = await _agencyQueryRepository.GetByIdAsync(command.AgencyId, cancellationToken);
            existing.AssignedAgencyId = command.AgencyId;
            existing.AssignedAgencyName = agency?.Name ?? "Assigned Agency";
            existing.AssignedPersonnel = command.PersonnelNames ?? new List<string>();
            existing.ScheduledDate = command.ScheduledDate;
            existing.ScheduledTime = command.ScheduledTimeSlot;
            existing.Status = "Assigned";

            var updated = await _commandRepository.UpdateRequestAsync(existing, cancellationToken);
            return updated == null ? null : MapToDetailDto(updated);
        }

        public async Task<MaintenanceFeedbackDto?> AddFeedbackAsync(AddMaintenanceFeedbackCommand command, CancellationToken cancellationToken = default)
        {
            var existing = await _queryRepository.GetByIdAsync(command.MaintenanceId, cancellationToken);
            if (existing == null) return null;

            var fb = new MaintenanceFeedbackEntity
            {
                Id = $"fb-{DateTime.UtcNow.Ticks}",
                MaintenanceRequestId = command.MaintenanceId,
                AdminName = command.AdminName ?? "Super Admin",
                Message = command.Message.Trim(),
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm"),
                Status = command.Status ?? "Sent"
            };

            existing.AdminFeedback.Add(fb);
            await _commandRepository.UpdateRequestAsync(existing, cancellationToken);

            return new MaintenanceFeedbackDto
            {
                Id = fb.Id,
                AdminName = fb.AdminName,
                Message = fb.Message,
                Timestamp = fb.Timestamp,
                Status = fb.Status
            };
        }

        public Task<bool> DeleteTicketAsync(DeleteMaintenanceTicketCommand command, CancellationToken cancellationToken = default)
        {
            return _commandRepository.DeleteRequestAsync(command.Id, cancellationToken);
        }

        public static MaintenanceTicketDetailDto MapToDetailDto(MaintenanceRequestEntity r)
        {
            return new MaintenanceTicketDetailDto
            {
                Id = r.Id,
                TicketNumber = r.TicketNumber,
                Title = r.Title,
                Description = r.Description,
                Category = r.Category,
                Priority = r.Priority,
                Status = r.Status,
                ReportedDate = r.ReportedDate,
                ApartmentId = r.PropertyId,
                ApartmentName = r.PropertyName,
                BlockId = r.BlockId,
                BlockName = r.BlockName,
                FloorId = r.FloorId,
                FlatNumber = r.FlatNumber,
                LocationDescription = r.LocationDescription,
                AssignedAgencyId = r.AssignedAgencyId,
                AssignedAgencyName = r.AssignedAgencyName,
                AssignedPersonnel = r.AssignedPersonnel ?? new List<string>(),
                ScheduledDate = r.ScheduledDate,
                ScheduledTime = r.ScheduledTime,
                ResolutionNotes = r.ResolutionNotes,
                CompletedDate = r.CompletedDate,
                AdminFeedback = r.AdminFeedback.Select(f => new MaintenanceFeedbackDto
                {
                    Id = f.Id,
                    AdminName = f.AdminName,
                    Message = f.Message,
                    Timestamp = f.Timestamp,
                    Status = f.Status
                }).ToList()
            };
        }

        public static MaintenanceTicketDto MapToDto(MaintenanceRequestEntity r)
        {
            return new MaintenanceTicketDto
            {
                Id = r.Id,
                TicketNumber = r.TicketNumber,
                Title = r.Title,
                Description = r.Description,
                Category = r.Category,
                Priority = r.Priority,
                Status = r.Status,
                ReportedDate = r.ReportedDate,
                ApartmentId = r.PropertyId,
                ApartmentName = r.PropertyName,
                BlockId = r.BlockId,
                BlockName = r.BlockName,
                FloorId = r.FloorId,
                FlatNumber = r.FlatNumber,
                LocationDescription = r.LocationDescription,
                AssignedAgencyId = r.AssignedAgencyId,
                AssignedAgencyName = r.AssignedAgencyName,
                AssignedPersonnel = r.AssignedPersonnel ?? new List<string>(),
                ScheduledDate = r.ScheduledDate,
                ScheduledTime = r.ScheduledTime
            };
        }
    }
}
