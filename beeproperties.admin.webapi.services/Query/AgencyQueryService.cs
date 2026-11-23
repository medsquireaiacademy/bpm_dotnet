using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Query;
using beeproperties.admin.webapi.repository.Entities;
using beeproperties.admin.webapi.repository.Query.Interface;
using beeproperties.admin.webapi.services.Commands;
using beeproperties.admin.webapi.services.Query.Interface;

namespace beeproperties.admin.webapi.services.Query
{
    public class AgencyQueryService : IAgencyQueryService
    {
        private readonly IAgencyQueryRepository _queryRepository;

        public AgencyQueryService(IAgencyQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<List<AgencyResponse>> GetAllAgenciesAsync(GetAllAgenciesQuery query, CancellationToken cancellationToken = default)
        {
            var entities = await _queryRepository.GetAllAsync(cancellationToken);

            var filtered = entities.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                var term = query.SearchTerm.Trim().ToLowerInvariant();
                filtered = filtered.Where(a =>
                    a.Name.ToLowerInvariant().Contains(term) ||
                    a.Category.ToLowerInvariant().Contains(term) ||
                    a.ContactPerson.ToLowerInvariant().Contains(term) ||
                    a.Phone.ToLowerInvariant().Contains(term) ||
                    a.Email.ToLowerInvariant().Contains(term) ||
                    (a.Company != null && a.Company.City.ToLowerInvariant().Contains(term)) ||
                    (a.Company != null && a.Company.Name.ToLowerInvariant().Contains(term)));
            }

            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                var cat = query.Category.Trim();
                filtered = filtered.Where(a =>
                    a.Category.Equals(cat, StringComparison.OrdinalIgnoreCase) ||
                    (a.Categories != null && a.Categories.Any(c => c.Equals(cat, StringComparison.OrdinalIgnoreCase))));
            }

            if (!string.IsNullOrWhiteSpace(query.AgreementStatus))
            {
                var status = query.AgreementStatus.Trim();
                filtered = filtered.Where(a => a.AgreementStatus.Contains(status, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(query.AvailabilityStatus))
            {
                filtered = filtered.Where(a => a.AvailabilityStatus.Equals(query.AvailabilityStatus.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(query.City))
            {
                filtered = filtered.Where(a => a.Company != null && a.Company.City.Equals(query.City.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(query.State))
            {
                filtered = filtered.Where(a => a.Company != null && a.Company.State.Equals(query.State.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            return filtered.Select(MapToResponse).ToList();
        }

        public async Task<AgencyDetailResponse?> GetAgencyByIdAsync(GetAgencyByIdQuery query, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(query.Id))
            {
                return null;
            }

            var entity = await _queryRepository.GetByIdAsync(query.Id, cancellationToken);
            return entity == null ? null : AgencyCommandService.MapToDetailResponse(entity);
        }

        public Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
        {
            return _queryRepository.ExistsAsync(id, cancellationToken);
        }

        public async Task<List<AgencyPersonnelDto>> GetAgencyPersonnelAsync(GetAgencyPersonnelQuery query, CancellationToken cancellationToken = default)
        {
            var agency = await _queryRepository.GetByIdAsync(query.AgencyId, cancellationToken);
            if (agency == null) return new List<AgencyPersonnelDto>();

            var personnel = agency.Personnel.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(query.Status))
            {
                personnel = personnel.Where(p => p.Status.Equals(query.Status, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(query.Shift))
            {
                personnel = personnel.Where(p => p.CurrentShift.Contains(query.Shift, StringComparison.OrdinalIgnoreCase));
            }

            return personnel.Select(p => new AgencyPersonnelDto
            {
                Id = p.Id,
                Name = p.Name,
                Role = p.Role,
                Experience = p.Experience,
                Phone = p.Phone,
                Email = p.Email,
                Status = p.Status,
                CurrentShift = p.CurrentShift
            }).ToList();
        }

        public async Task<List<AgencyPropertyAssignmentDto>> GetAgencyPropertyAssignmentsAsync(GetAgencyPropertyAssignmentsQuery query, CancellationToken cancellationToken = default)
        {
            var agency = await _queryRepository.GetByIdAsync(query.AgencyId, cancellationToken);
            if (agency == null) return new List<AgencyPropertyAssignmentDto>();

            return agency.PropertyAssignments.Select(pa => new AgencyPropertyAssignmentDto
            {
                Id = pa.Id,
                Organization = pa.Organization,
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
                ApprovedServices = pa.ApprovedServices ?? new List<string>(),
                ApprovedBlocks = pa.ApprovedBlocks ?? new List<string>(),
                PropertyContactPerson = pa.PropertyContactPerson,
                PropertyContactNumber = pa.PropertyContactNumber,
                PropertyManagerSponsor = pa.PropertyManagerSponsor,
                SupportingDocs = pa.SupportingDocs ?? new List<string>(),
                Reason = pa.Reason,
                Status = pa.Status,
                ReviewDate = pa.ReviewDate,
                Reviewer = pa.Reviewer,
                Comments = pa.Comments,
                Restrictions = pa.Restrictions
            }).ToList();
        }

        public async Task<List<AgencyApprovalHistoryDto>> GetAgencyApprovalHistoryAsync(GetAgencyApprovalHistoryQuery query, CancellationToken cancellationToken = default)
        {
            var agency = await _queryRepository.GetByIdAsync(query.AgencyId, cancellationToken);
            if (agency == null) return new List<AgencyApprovalHistoryDto>();

            return agency.ApprovalHistory.Select(ah => new AgencyApprovalHistoryDto
            {
                Id = ah.Id,
                Timestamp = ah.Timestamp,
                Type = ah.Type,
                Decision = ah.Decision,
                Reviewer = ah.Reviewer,
                Comments = ah.Comments,
                ChangesRequested = ah.ChangesRequested
            }).ToList();
        }

        private static AgencyResponse MapToResponse(AgencyEntity entity)
        {
            return new AgencyResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Category = entity.Category,
                Categories = entity.Categories ?? new List<string>(),
                LogoText = entity.LogoText,
                ContactPerson = entity.ContactPerson,
                Phone = entity.Phone,
                Email = entity.Email,
                AgreementStatus = entity.AgreementStatus,
                AvailabilityStatus = entity.AvailabilityStatus,
                TotalPersonnel = entity.TotalPersonnel > 0 ? entity.TotalPersonnel : entity.Personnel.Count,
                AllocatedPersonnel = entity.AllocatedPersonnel,
                AvailablePersonnel = entity.AvailablePersonnel,
                OnLeavePersonnel = entity.OnLeavePersonnel,
                ActiveAssignmentsCount = entity.ActiveAssignmentsCount > 0 ? entity.ActiveAssignmentsCount : entity.Assignments.Count,
                RegistrationRefNumber = entity.RegistrationRefNumber,
                RegistrationStatus = entity.RegistrationStatus,
                SubmissionDate = entity.SubmissionDate,
                TermsAccepted = entity.TermsAccepted,
                DeclarationAccepted = entity.DeclarationAccepted,
                City = entity.Company?.City ?? string.Empty,
                State = entity.Company?.State ?? string.Empty,
                TotalServices = entity.Services.Count,
                TotalDocuments = entity.Documents.Count,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}

