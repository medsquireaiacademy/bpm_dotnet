using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.repository.Query.Interface;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.services.Query
{
    public class DashboardQueryService : IDashboardQueryService
    {
        private readonly IPropertyQueryRepository _propertyRepository;
        private readonly IAgencyQueryRepository _agencyRepository;
        private readonly IMaintenanceQueryRepository _maintenanceRepository;

        public DashboardQueryService(
            IPropertyQueryRepository propertyRepository,
            IAgencyQueryRepository agencyRepository,
            IMaintenanceQueryRepository maintenanceRepository)
        {
            _propertyRepository = propertyRepository;
            _agencyRepository = agencyRepository;
            _maintenanceRepository = maintenanceRepository;
        }

        public async Task<DashboardMetricsDto> GetDashboardMetricsAsync(GetDashboardMetricsQuery query, CancellationToken cancellationToken = default)
        {
            var properties = await _propertyRepository.GetAllAsync(cancellationToken);
            var agencies = await _agencyRepository.GetAllAsync(cancellationToken);
            var tickets = await _maintenanceRepository.GetAllAsync(cancellationToken);

            var totalApartments = properties.Count;
            var totalBlocks = properties.Sum(p => p.Blocks.Count);
            var totalFlats = properties.Sum(p => p.Blocks.Sum(b => b.FlatsCount));
            var occupiedFlats = properties.Sum(p => p.Blocks.Sum(b => b.OccupiedFlats));
            var vacantFlats = properties.Sum(p => p.Blocks.Sum(b => b.VacantFlats));
            var occupancyRate = totalFlats > 0 ? Math.Round(((double)occupiedFlats / totalFlats) * 100, 1) : 0;

            var activeAgencies = agencies.Count(a => a.AgreementStatus.Equals("Active", StringComparison.OrdinalIgnoreCase));
            var availableAgencies = agencies.Count(a => a.AvailabilityStatus.Equals("Available", StringComparison.OrdinalIgnoreCase) || a.AvailablePersonnel > 0);

            var pendingAgencies = agencies.Count(a => a.RegistrationStatus.Equals("Pending Approval", StringComparison.OrdinalIgnoreCase) || a.RegistrationStatus.Equals("Submitted", StringComparison.OrdinalIgnoreCase));
            var pendingAssignments = agencies.Sum(a => a.PropertyAssignments.Count(pa => pa.Status.Equals("Pending Approval", StringComparison.OrdinalIgnoreCase)));
            var pendingApprovals = pendingAgencies + pendingAssignments;

            var activeMaintenance = tickets.Count(t => !t.Status.Equals("Completed", StringComparison.OrdinalIgnoreCase) && !t.Status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase));
            var totalAllocatedPersonnel = agencies.Sum(a => a.AllocatedPersonnel > 0 ? a.AllocatedPersonnel : a.Personnel.Count(p => p.Status == "Allocated" || p.Status == "On Duty"));

            return new DashboardMetricsDto
            {
                TotalApartments = totalApartments,
                TotalBlocks = totalBlocks,
                TotalFlats = totalFlats,
                OccupiedFlats = occupiedFlats,
                VacantFlats = vacantFlats,
                OccupancyRate = occupancyRate,
                ActiveAgencies = activeAgencies,
                AvailableAgencies = availableAgencies,
                PendingApprovals = pendingApprovals,
                ActiveMaintenanceRequests = activeMaintenance,
                TotalAllocatedPersonnel = totalAllocatedPersonnel
            };
        }

        public async Task<ServiceRequestChartsDto> GetServiceRequestChartsAsync(GetServiceRequestChartsQuery query, CancellationToken cancellationToken = default)
        {
            var tickets = await _maintenanceRepository.GetAllAsync(cancellationToken);
            if (!string.IsNullOrWhiteSpace(query.PropertyId))
            {
                tickets = tickets.Where(t => t.PropertyId.Equals(query.PropertyId, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var total = tickets.Count;
            var completed = tickets.Count(t => t.Status.Equals("Completed", StringComparison.OrdinalIgnoreCase));
            var inProgress = tickets.Count(t => t.Status.Equals("In Progress", StringComparison.OrdinalIgnoreCase));
            var assigned = tickets.Count(t => t.Status.Equals("Assigned", StringComparison.OrdinalIgnoreCase));
            var reported = tickets.Count(t => t.Status.Equals("Reported", StringComparison.OrdinalIgnoreCase));
            var open = total - completed;

            var statusItems = new List<ChartStatusBreakdownItem>
            {
                new() { Name = "Completed", Value = completed, Color = "#10b981", Pct = total > 0 ? (int)Math.Round((double)completed / total * 100) : 0 },
                new() { Name = "In Progress", Value = inProgress, Color = "#3b82f6", Pct = total > 0 ? (int)Math.Round((double)inProgress / total * 100) : 0 },
                new() { Name = "Assigned", Value = assigned, Color = "#f59e0b", Pct = total > 0 ? (int)Math.Round((double)assigned / total * 100) : 0 },
                new() { Name = "Reported", Value = reported, Color = "#ef4444", Pct = total > 0 ? (int)Math.Round((double)reported / total * 100) : 0 }
            };

            var categories = tickets
                .GroupBy(t => t.Category)
                .Select(g => new ChartCategoryItem
                {
                    Category = g.Key,
                    Count = g.Count(),
                    Completed = g.Count(x => x.Status.Equals("Completed", StringComparison.OrdinalIgnoreCase)),
                    Pending = g.Count(x => !x.Status.Equals("Completed", StringComparison.OrdinalIgnoreCase))
                })
                .OrderByDescending(c => c.Count)
                .Take(6)
                .ToList();

            return new ServiceRequestChartsDto
            {
                StatusData = statusItems,
                CategoryData = categories,
                TotalRequests = total,
                CompletedRequests = completed,
                OpenRequests = open,
                ResolutionRate = total > 0 ? (int)Math.Round((double)completed / total * 100) : 0
            };
        }

        public async Task<PendingApprovalsSummaryDto> GetPendingApprovalsAsync(GetPendingApprovalsQuery query, CancellationToken cancellationToken = default)
        {
            var agencies = await _agencyRepository.GetAllAsync(cancellationToken);

            var pendingRegs = agencies
                .Where(a => a.RegistrationStatus.Equals("Pending Approval", StringComparison.OrdinalIgnoreCase) ||
                            a.RegistrationStatus.Equals("Submitted", StringComparison.OrdinalIgnoreCase))
                .Select(a => new AgencyResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                    Category = a.Category,
                    Categories = a.Categories ?? new List<string>(),
                    ContactPerson = a.ContactPerson,
                    Phone = a.Phone,
                    Email = a.Email,
                    AgreementStatus = a.AgreementStatus,
                    AvailabilityStatus = a.AvailabilityStatus,
                    RegistrationRefNumber = a.RegistrationRefNumber,
                    RegistrationStatus = a.RegistrationStatus,
                    SubmissionDate = a.SubmissionDate
                })
                .ToList();

            var pendingAssignments = new List<PendingPropertyAssignmentDto>();
            foreach (var a in agencies)
            {
                foreach (var pa in a.PropertyAssignments.Where(p => p.Status.Equals("Pending Approval", StringComparison.OrdinalIgnoreCase)))
                {
                    pendingAssignments.Add(new PendingPropertyAssignmentDto
                    {
                        Id = pa.Id,
                        AgencyId = a.Id,
                        AgencyName = a.Name,
                        PropertyId = pa.PropertyId,
                        PropertyName = pa.PropertyName,
                        PropertyCode = pa.PropertyCode,
                        PropertyType = pa.PropertyType,
                        PropertyAddress = pa.PropertyAddress,
                        City = pa.City,
                        State = pa.State,
                        AssignmentType = pa.AssignmentType,
                        AssignedBlocks = pa.AssignedBlocks ?? new List<string>(),
                        AssignedFloorsZones = pa.AssignedFloorsZones,
                        AssignedFacilitiesAreas = pa.AssignedFacilitiesAreas ?? new List<string>(),
                        StartDate = pa.StartDate,
                        EndDate = pa.EndDate,
                        RequestedServices = pa.RequestedServices ?? new List<string>(),
                        PropertyContactPerson = pa.PropertyContactPerson,
                        PropertyContactNumber = pa.PropertyContactNumber,
                        PropertyManagerSponsor = pa.PropertyManagerSponsor,
                        SupportingDocs = pa.SupportingDocs ?? new List<string>(),
                        Reason = pa.Reason,
                        Status = pa.Status
                    });
                }
            }

            return new PendingApprovalsSummaryDto
            {
                PendingRegistrations = pendingRegs,
                PendingPropertyAssignments = pendingAssignments
            };
        }
    }
}
