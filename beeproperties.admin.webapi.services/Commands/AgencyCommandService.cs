using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.repository.Commands.Interface;
using beeproperties.admin.webapi.repository.Entities;
using beeproperties.admin.webapi.repository.Query.Interface;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.services.Commands
{
    public class AgencyCommandService : IAgencyCommandService
    {
        private readonly IAgencyCommandRepository _commandRepository;
        private readonly IAgencyQueryRepository _queryRepository;

        public AgencyCommandService(
            IAgencyCommandRepository commandRepository,
            IAgencyQueryRepository queryRepository)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
        }

        public async Task<AgencyDetailResponse> CreateAgencyAsync(CreateAgencyCommand command, CancellationToken cancellationToken = default)
        {
            var entity = new AgencyEntity
            {
                Id = string.IsNullOrWhiteSpace(command.Id) ? Guid.NewGuid().ToString("N") : command.Id.Trim(),
                Name = command.Name.Trim(),
                Category = command.Category.Trim(),
                Categories = command.Categories ?? new List<string>(),
                LogoText = command.LogoText.Trim(),
                ContactPerson = command.ContactPerson.Trim(),
                Phone = command.Phone.Trim(),
                Email = command.Email.Trim(),
                AgreementStatus = command.AgreementStatus.Trim(),
                AvailabilityStatus = command.AvailabilityStatus.Trim(),
                TotalPersonnel = command.TotalPersonnel,
                AllocatedPersonnel = command.AllocatedPersonnel,
                AvailablePersonnel = command.AvailablePersonnel,
                OnLeavePersonnel = command.OnLeavePersonnel,
                ActiveAssignmentsCount = command.ActiveAssignmentsCount,
                RegistrationRefNumber = command.RegistrationRefNumber.Trim(),
                RegistrationStatus = command.RegistrationStatus.Trim(),
                SubmissionDate = command.SubmissionDate.Trim(),
                TermsAccepted = command.TermsAccepted,
                DeclarationAccepted = command.DeclarationAccepted,
                Agent = command.Agent == null ? null : MapAgentDtoToEntity(command.Agent),
                Company = command.Company == null ? null : MapCompanyDtoToEntity(command.Company),
                Contacts = command.Contacts == null ? null : MapContactsDtoToEntity(command.Contacts),
                Personnel = (command.Personnel ?? new List<AgencyPersonnelDto>()).Select(MapPersonnelDtoToEntity).ToList(),
                Assignments = (command.Assignments ?? new List<AgencyAssignmentDto>()).Select(MapAssignmentDtoToEntity).ToList(),
                Services = (command.Services ?? new List<AgencyServiceDto>()).Select(MapServiceDtoToEntity).ToList(),
                Documents = (command.Documents ?? new List<AgencyDocumentDto>()).Select(MapDocumentDtoToEntity).ToList(),
                PropertyAssignments = (command.PropertyAssignments ?? new List<AgencyPropertyAssignmentDto>()).Select(MapPropertyAssignmentDtoToEntity).ToList(),
                ApprovalHistory = (command.ApprovalHistory ?? new List<AgencyApprovalHistoryDto>()).Select(MapApprovalHistoryDtoToEntity).ToList()
            };

            var added = await _commandRepository.AddAsync(entity, cancellationToken);
            return MapToDetailResponse(added);
        }

        public async Task<AgencyDetailResponse?> UpdateAgencyAsync(UpdateAgencyCommand command, CancellationToken cancellationToken = default)
        {
            var existing = await _queryRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existing == null)
            {
                return null;
            }

            existing.Name = command.Name.Trim();
            existing.Category = command.Category.Trim();
            if (command.Categories != null)
            {
                existing.Categories = command.Categories;
            }
            existing.LogoText = command.LogoText.Trim();
            existing.ContactPerson = command.ContactPerson.Trim();
            existing.Phone = command.Phone.Trim();
            existing.Email = command.Email.Trim();
            existing.AgreementStatus = command.AgreementStatus.Trim();
            existing.AvailabilityStatus = command.AvailabilityStatus.Trim();
            existing.TotalPersonnel = command.TotalPersonnel;
            existing.AllocatedPersonnel = command.AllocatedPersonnel;
            existing.AvailablePersonnel = command.AvailablePersonnel;
            existing.OnLeavePersonnel = command.OnLeavePersonnel;
            existing.ActiveAssignmentsCount = command.ActiveAssignmentsCount;
            existing.RegistrationRefNumber = command.RegistrationRefNumber.Trim();
            existing.RegistrationStatus = command.RegistrationStatus.Trim();
            existing.SubmissionDate = command.SubmissionDate.Trim();
            existing.TermsAccepted = command.TermsAccepted;
            existing.DeclarationAccepted = command.DeclarationAccepted;

            if (command.Agent != null)
            {
                existing.Agent = MapAgentDtoToEntity(command.Agent);
            }

            if (command.Company != null)
            {
                existing.Company = MapCompanyDtoToEntity(command.Company);
            }

            if (command.Contacts != null)
            {
                existing.Contacts = MapContactsDtoToEntity(command.Contacts);
            }

            if (command.Personnel != null)
            {
                existing.Personnel = command.Personnel.Select(MapPersonnelDtoToEntity).ToList();
            }

            if (command.Assignments != null)
            {
                existing.Assignments = command.Assignments.Select(MapAssignmentDtoToEntity).ToList();
            }

            if (command.Services != null)
            {
                existing.Services = command.Services.Select(MapServiceDtoToEntity).ToList();
            }

            if (command.Documents != null)
            {
                existing.Documents = command.Documents.Select(MapDocumentDtoToEntity).ToList();
            }

            if (command.PropertyAssignments != null)
            {
                existing.PropertyAssignments = command.PropertyAssignments.Select(MapPropertyAssignmentDtoToEntity).ToList();
            }

            if (command.ApprovalHistory != null)
            {
                existing.ApprovalHistory = command.ApprovalHistory.Select(MapApprovalHistoryDtoToEntity).ToList();
            }

            var updated = await _commandRepository.UpdateAsync(existing, cancellationToken);
            return updated == null ? null : MapToDetailResponse(updated);
        }

        public Task<bool> DeleteAgencyAsync(DeleteAgencyCommand command, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(command.Id))
            {
                return Task.FromResult(false);
            }

            return _commandRepository.DeleteAsync(command.Id, cancellationToken);
        }

        public async Task<AgencyDetailResponse?> ReviewRegistrationAsync(ReviewAgencyRegistrationCommand command, CancellationToken cancellationToken = default)
        {
            var agency = await _queryRepository.GetByIdAsync(command.AgencyId, cancellationToken);
            if (agency == null) return null;

            agency.RegistrationStatus = command.Decision switch
            {
                "Approve" => "Approved",
                "Reject" => "Rejected",
                "Request Changes" => "Changes Requested",
                _ => command.Decision
            };

            if (command.Decision == "Approve")
            {
                agency.AgreementStatus = "Active";
                agency.AvailabilityStatus = "Available";
            }

            agency.ApprovalHistory.Insert(0, new AgencyApprovalHistoryEntity
            {
                Id = $"appr-{DateTime.UtcNow.Ticks}",
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm"),
                Type = "Agency Registration Review",
                Decision = command.Decision,
                Reviewer = "Super Admin",
                Comments = command.Comments ?? string.Empty,
                ChangesRequested = command.ChangesRequested
            });

            var updated = await _commandRepository.UpdateAsync(agency, cancellationToken);
            return updated == null ? null : MapToDetailResponse(updated);
        }

        public async Task<AgencyPersonnelDto?> AddPersonnelAsync(CreateAgencyPersonnelCommand command, CancellationToken cancellationToken = default)
        {
            var agency = await _queryRepository.GetByIdAsync(command.AgencyId, cancellationToken);
            if (agency == null) return null;

            var personnel = new AgencyPersonnelEntity
            {
                Id = $"staff-{DateTime.UtcNow.Ticks}",
                Name = command.Name.Trim(),
                Role = command.Role.Trim(),
                Experience = command.Experience ?? "5 yrs",
                Phone = command.Phone ?? string.Empty,
                Email = command.Email ?? string.Empty,
                Status = command.Status ?? "Available",
                CurrentShift = command.CurrentShift ?? "Unassigned"
            };

            agency.Personnel.Add(personnel);
            agency.TotalPersonnel = agency.Personnel.Count;
            agency.AvailablePersonnel = agency.Personnel.Count(p => p.Status == "Available");
            agency.AllocatedPersonnel = agency.Personnel.Count(p => p.Status == "Allocated" || p.Status == "On Duty");
            agency.OnLeavePersonnel = agency.Personnel.Count(p => p.Status == "On Leave");

            await _commandRepository.UpdateAsync(agency, cancellationToken);

            return new AgencyPersonnelDto
            {
                Id = personnel.Id,
                Name = personnel.Name,
                Role = personnel.Role,
                Experience = personnel.Experience,
                Phone = personnel.Phone,
                Email = personnel.Email,
                Status = personnel.Status,
                CurrentShift = personnel.CurrentShift
            };
        }

        public async Task<AgencyPersonnelDto?> UpdatePersonnelAsync(UpdateAgencyPersonnelCommand command, CancellationToken cancellationToken = default)
        {
            var agency = await _queryRepository.GetByIdAsync(command.AgencyId, cancellationToken);
            if (agency == null) return null;

            var existing = agency.Personnel.FirstOrDefault(p => p.Id == command.PersonnelId);
            if (existing == null) return null;

            existing.Name = command.Name.Trim();
            existing.Role = command.Role.Trim();
            if (!string.IsNullOrEmpty(command.Experience)) existing.Experience = command.Experience;
            if (command.Phone != null) existing.Phone = command.Phone;
            if (command.Email != null) existing.Email = command.Email;
            if (!string.IsNullOrEmpty(command.Status)) existing.Status = command.Status;
            if (!string.IsNullOrEmpty(command.CurrentShift)) existing.CurrentShift = command.CurrentShift;

            agency.TotalPersonnel = agency.Personnel.Count;
            agency.AvailablePersonnel = agency.Personnel.Count(p => p.Status == "Available");
            agency.AllocatedPersonnel = agency.Personnel.Count(p => p.Status == "Allocated" || p.Status == "On Duty");
            agency.OnLeavePersonnel = agency.Personnel.Count(p => p.Status == "On Leave");

            await _commandRepository.UpdateAsync(agency, cancellationToken);

            return new AgencyPersonnelDto
            {
                Id = existing.Id,
                Name = existing.Name,
                Role = existing.Role,
                Experience = existing.Experience,
                Phone = existing.Phone,
                Email = existing.Email,
                Status = existing.Status,
                CurrentShift = existing.CurrentShift
            };
        }

        public async Task<bool> DeletePersonnelAsync(DeleteAgencyPersonnelCommand command, CancellationToken cancellationToken = default)
        {
            var agency = await _queryRepository.GetByIdAsync(command.AgencyId, cancellationToken);
            if (agency == null) return false;

            var removed = agency.Personnel.RemoveAll(p => p.Id == command.PersonnelId) > 0;
            if (removed)
            {
                agency.TotalPersonnel = agency.Personnel.Count;
                agency.AvailablePersonnel = agency.Personnel.Count(p => p.Status == "Available");
                agency.AllocatedPersonnel = agency.Personnel.Count(p => p.Status == "Allocated" || p.Status == "On Duty");
                agency.OnLeavePersonnel = agency.Personnel.Count(p => p.Status == "On Leave");
                await _commandRepository.UpdateAsync(agency, cancellationToken);
            }
            return removed;
        }

        public async Task<AgencyPropertyAssignmentDto?> RequestPropertyAssignmentAsync(RequestPropertyAssignmentCommand command, CancellationToken cancellationToken = default)
        {
            var agency = await _queryRepository.GetByIdAsync(command.AgencyId, cancellationToken);
            if (agency == null) return null;

            var assignment = new AgencyPropertyAssignmentEntity
            {
                Id = $"asgn-{DateTime.UtcNow.Ticks}",
                Organization = command.Organization,
                PropertyId = command.PropertyId,
                PropertyName = command.PropertyName,
                PropertyCode = command.PropertyCode ?? string.Empty,
                PropertyType = command.PropertyType ?? string.Empty,
                PropertyAddress = command.PropertyAddress ?? string.Empty,
                City = command.City ?? string.Empty,
                State = command.State ?? string.Empty,
                AssignmentType = command.AssignmentType,
                AssignedBlocks = command.AssignedBlocks ?? new List<string>(),
                AssignedFloorsZones = command.AssignedFloorsZones ?? string.Empty,
                AssignedFacilitiesAreas = command.AssignedFacilitiesAreas ?? new List<string>(),
                StartDate = command.StartDate,
                EndDate = command.EndDate ?? string.Empty,
                RequestedServices = command.RequestedServices ?? new List<string>(),
                PropertyContactPerson = command.PropertyContactPerson ?? string.Empty,
                PropertyContactNumber = command.PropertyContactNumber ?? string.Empty,
                PropertyManagerSponsor = command.PropertyManagerSponsor ?? string.Empty,
                SupportingDocs = command.SupportingDocs ?? new List<string>(),
                Reason = command.Reason ?? string.Empty,
                Status = "Pending Approval"
            };

            agency.PropertyAssignments.Add(assignment);
            await _commandRepository.UpdateAsync(agency, cancellationToken);

            return MapPropertyAssignmentEntityToDto(assignment);
        }

        public async Task<AgencyPropertyAssignmentDto?> ReviewPropertyAssignmentAsync(ReviewPropertyAssignmentCommand command, CancellationToken cancellationToken = default)
        {
            var agency = await _queryRepository.GetByIdAsync(command.AgencyId, cancellationToken);
            if (agency == null) return null;

            var assignment = agency.PropertyAssignments.FirstOrDefault(pa => pa.Id == command.AssignmentId);
            if (assignment == null) return null;

            assignment.Status = command.Decision switch
            {
                "Approve" => "Approved",
                "Approve with Restrictions" => "Approved with Restrictions",
                "Request Changes" => "Changes Requested",
                "Reject" => "Rejected",
                _ => command.Decision
            };

            assignment.ReviewDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm");
            assignment.Reviewer = "Super Admin";
            assignment.Comments = command.Comments ?? string.Empty;
            assignment.Restrictions = command.Restrictions ?? string.Empty;
            if (command.ApprovedServices.Any()) assignment.ApprovedServices = command.ApprovedServices;
            if (command.ApprovedBlocks.Any()) assignment.ApprovedBlocks = command.ApprovedBlocks;
            if (!string.IsNullOrEmpty(command.ApprovedStartDate)) assignment.StartDate = command.ApprovedStartDate;
            if (!string.IsNullOrEmpty(command.ApprovedEndDate)) assignment.EndDate = command.ApprovedEndDate;

            agency.ActiveAssignmentsCount = agency.PropertyAssignments.Count(pa => pa.Status.StartsWith("Approved", StringComparison.OrdinalIgnoreCase) || pa.Status.Equals("Active", StringComparison.OrdinalIgnoreCase));

            agency.ApprovalHistory.Insert(0, new AgencyApprovalHistoryEntity
            {
                Id = $"appr-{DateTime.UtcNow.Ticks}",
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm"),
                Type = $"Property Assignment Review ({assignment.PropertyName})",
                Decision = command.Decision,
                Reviewer = "Super Admin",
                Comments = command.Comments ?? string.Empty,
                ChangesRequested = command.Restrictions
            });

            await _commandRepository.UpdateAsync(agency, cancellationToken);

            return MapPropertyAssignmentEntityToDto(assignment);
        }

        public async Task<bool> RevokePropertyAssignmentAsync(RevokePropertyAssignmentCommand command, CancellationToken cancellationToken = default)
        {
            var agency = await _queryRepository.GetByIdAsync(command.AgencyId, cancellationToken);
            if (agency == null) return false;

            var assignment = agency.PropertyAssignments.FirstOrDefault(pa => pa.Id == command.AssignmentId);
            if (assignment == null) return false;

            assignment.Status = "Terminated";
            agency.ActiveAssignmentsCount = agency.PropertyAssignments.Count(pa => pa.Status.StartsWith("Approved", StringComparison.OrdinalIgnoreCase) || pa.Status.Equals("Active", StringComparison.OrdinalIgnoreCase));

            agency.ApprovalHistory.Insert(0, new AgencyApprovalHistoryEntity
            {
                Id = $"appr-{DateTime.UtcNow.Ticks}",
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm"),
                Type = $"Revocation of Assignment ({assignment.PropertyName})",
                Decision = "Revoked",
                Reviewer = "Super Admin",
                Comments = "Assignment revoked by administrator."
            });

            await _commandRepository.UpdateAsync(agency, cancellationToken);
            return true;
        }

        public async Task<AgencyServiceDto?> AddServiceAsync(CreateAgencyServiceCommand command, CancellationToken cancellationToken = default)
        {
            var agency = await _queryRepository.GetByIdAsync(command.AgencyId, cancellationToken);
            if (agency == null) return null;

            var s = new AgencyServiceEntity
            {
                Id = $"srv-{DateTime.UtcNow.Ticks}",
                Category = command.Category.Trim(),
                Specialization = command.Specialization ?? string.Empty,
                Description = command.Description ?? string.Empty,
                CoverageArea = command.CoverageArea ?? string.Empty,
                ExperienceYears = command.YearsOfExperience,
                Certifications = command.Certifications ?? string.Empty,
                Availability = command.Availability ?? "Full-time"
            };

            agency.Services.Add(s);
            if (!agency.Categories.Contains(s.Category, StringComparer.OrdinalIgnoreCase))
            {
                agency.Categories.Add(s.Category);
            }

            await _commandRepository.UpdateAsync(agency, cancellationToken);

            return new AgencyServiceDto
            {
                Id = s.Id,
                Category = s.Category,
                Specialization = s.Specialization,
                Description = s.Description,
                CoverageArea = s.CoverageArea,
                ExperienceYears = s.ExperienceYears,
                Certifications = s.Certifications,
                Availability = s.Availability
            };
        }

        public async Task<bool> DeleteServiceAsync(DeleteAgencyServiceCommand command, CancellationToken cancellationToken = default)
        {
            var agency = await _queryRepository.GetByIdAsync(command.AgencyId, cancellationToken);
            if (agency == null) return false;

            var removed = agency.Services.RemoveAll(s => s.Id == command.ServiceId) > 0;
            if (removed)
            {
                agency.Categories = agency.Services.Select(s => s.Category).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                await _commandRepository.UpdateAsync(agency, cancellationToken);
            }
            return removed;
        }

        public async Task<AgencyDocumentDto?> AddDocumentAsync(AddAgencyDocumentCommand command, CancellationToken cancellationToken = default)
        {
            var agency = await _queryRepository.GetByIdAsync(command.AgencyId, cancellationToken);
            if (agency == null) return null;

            var doc = new AgencyDocumentEntity
            {
                Id = $"doc-{DateTime.UtcNow.Ticks}",
                DocumentType = command.DocumentType,
                FileName = command.FileName,
                FileUrl = command.FileUrl,
                UploadDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                VerificationStatus = "Verified",
                Remarks = command.Description ?? string.Empty
            };

            agency.Documents.Add(doc);
            await _commandRepository.UpdateAsync(agency, cancellationToken);

            return new AgencyDocumentDto
            {
                Id = doc.Id,
                DocumentType = doc.DocumentType,
                FileName = doc.FileName,
                FileUrl = doc.FileUrl,
                UploadDate = doc.UploadDate,
                VerificationStatus = doc.VerificationStatus,
                Remarks = doc.Remarks
            };
        }

        public async Task<bool> DeleteDocumentAsync(DeleteAgencyDocumentCommand command, CancellationToken cancellationToken = default)
        {
            var agency = await _queryRepository.GetByIdAsync(command.AgencyId, cancellationToken);
            if (agency == null) return false;

            var removed = agency.Documents.RemoveAll(d => d.Id == command.DocumentId) > 0;
            if (removed)
            {
                await _commandRepository.UpdateAsync(agency, cancellationToken);
            }
            return removed;
        }

        private static AgencyPropertyAssignmentDto MapPropertyAssignmentEntityToDto(AgencyPropertyAssignmentEntity pa)
        {
            return new AgencyPropertyAssignmentDto
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
            };
        }

        private static AgencyAgentEntity MapAgentDtoToEntity(AgencyAgentDto dto)
        {
            return new AgencyAgentEntity
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                ProfilePhoto = dto.ProfilePhoto,
                Designation = dto.Designation,
                Phone = dto.Phone,
                AlternatePhone = dto.AlternatePhone,
                Email = dto.Email,
                PreferredLanguage = dto.PreferredLanguage,
                AgentType = dto.AgentType,
                EmployeeId = dto.EmployeeId,
                EmergencyContactName = dto.EmergencyContactName,
                EmergencyContactNumber = dto.EmergencyContactNumber
            };
        }

        private static AgencyCompanyEntity MapCompanyDtoToEntity(AgencyCompanyDto dto)
        {
            return new AgencyCompanyEntity
            {
                Name = dto.Name,
                RegisteredCompanyName = dto.RegisteredCompanyName,
                BusinessType = dto.BusinessType,
                RegistrationNumber = dto.RegistrationNumber,
                Gstin = dto.Gstin,
                Pan = dto.Pan,
                Website = dto.Website,
                YearOfEstablishment = dto.YearOfEstablishment,
                NumberOfEmployees = dto.NumberOfEmployees,
                CompanyEmail = dto.CompanyEmail,
                CompanyPhone = dto.CompanyPhone,
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                Country = dto.Country,
                State = dto.State,
                City = dto.City,
                PinCode = dto.PinCode,
                Description = dto.Description,
                AuthorizedSignatoryName = dto.AuthorizedSignatoryName,
                AuthorizedSignatoryDesignation = dto.AuthorizedSignatoryDesignation
            };
        }

        private static AgencyContactsEntity MapContactsDtoToEntity(AgencyContactsDto dto)
        {
            return new AgencyContactsEntity
            {
                PrimaryContactName = dto.PrimaryContactName,
                PrimaryContactDesignation = dto.PrimaryContactDesignation,
                PrimaryContactMobile = dto.PrimaryContactMobile,
                PrimaryContactEmail = dto.PrimaryContactEmail,
                AlternateContactPerson = dto.AlternateContactPerson,
                AlternateContactMobile = dto.AlternateContactMobile,
                AccountsBillingName = dto.AccountsBillingName,
                AccountsBillingEmail = dto.AccountsBillingEmail,
                AccountsBillingPhone = dto.AccountsBillingPhone,
                PreferredContactMethod = dto.PreferredContactMethod,
                PreferredContactTime = dto.PreferredContactTime
            };
        }

        private static AgencyPersonnelEntity MapPersonnelDtoToEntity(AgencyPersonnelDto dto)
        {
            return new AgencyPersonnelEntity
            {
                Id = string.IsNullOrWhiteSpace(dto.Id) ? Guid.NewGuid().ToString("N") : dto.Id,
                Name = dto.Name,
                Role = dto.Role,
                Experience = dto.Experience,
                Status = dto.Status,
                CurrentShift = dto.CurrentShift,
                Phone = dto.Phone
            };
        }

        private static AgencyAssignmentEntity MapAssignmentDtoToEntity(AgencyAssignmentDto dto)
        {
            return new AgencyAssignmentEntity
            {
                Id = string.IsNullOrWhiteSpace(dto.Id) ? Guid.NewGuid().ToString("N") : dto.Id,
                Property = dto.Property,
                Location = dto.Location,
                Type = dto.Type,
                Personnel = dto.Personnel ?? new List<string>(),
                Shift = dto.Shift,
                Date = dto.Date,
                Status = dto.Status
            };
        }

        private static AgencyServiceEntity MapServiceDtoToEntity(AgencyServiceDto dto)
        {
            return new AgencyServiceEntity
            {
                Id = string.IsNullOrWhiteSpace(dto.Id) ? Guid.NewGuid().ToString("N") : dto.Id,
                Category = dto.Category,
                Specialization = dto.Specialization,
                Description = dto.Description,
                OtherServiceName = dto.OtherServiceName,
                CoverageArea = dto.CoverageArea,
                ExperienceYears = dto.ExperienceYears,
                Certifications = dto.Certifications,
                Availability = dto.Availability
            };
        }

        private static AgencyDocumentEntity MapDocumentDtoToEntity(AgencyDocumentDto dto)
        {
            return new AgencyDocumentEntity
            {
                Id = string.IsNullOrWhiteSpace(dto.Id) ? Guid.NewGuid().ToString("N") : dto.Id,
                DocumentType = dto.DocumentType,
                FileName = dto.FileName,
                FileUrl = dto.FileUrl,
                UploadDate = dto.UploadDate,
                ExpiryDate = dto.ExpiryDate,
                VerificationStatus = dto.VerificationStatus,
                Remarks = dto.Remarks
            };
        }

        private static AgencyPropertyAssignmentEntity MapPropertyAssignmentDtoToEntity(AgencyPropertyAssignmentDto dto)
        {
            return new AgencyPropertyAssignmentEntity
            {
                Id = string.IsNullOrWhiteSpace(dto.Id) ? Guid.NewGuid().ToString("N") : dto.Id,
                Organization = dto.Organization,
                PropertyId = dto.PropertyId,
                PropertyName = dto.PropertyName,
                PropertyCode = dto.PropertyCode,
                PropertyType = dto.PropertyType,
                PropertyAddress = dto.PropertyAddress,
                City = dto.City,
                State = dto.State,
                AssignmentType = dto.AssignmentType,
                AssignedBlocks = dto.AssignedBlocks ?? new List<string>(),
                AssignedFloorsZones = dto.AssignedFloorsZones,
                AssignedFacilitiesAreas = dto.AssignedFacilitiesAreas ?? new List<string>(),
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                RequestedServices = dto.RequestedServices ?? new List<string>(),
                ApprovedServices = dto.ApprovedServices ?? new List<string>(),
                ApprovedBlocks = dto.ApprovedBlocks ?? new List<string>(),
                PropertyContactPerson = dto.PropertyContactPerson,
                PropertyContactNumber = dto.PropertyContactNumber,
                PropertyManagerSponsor = dto.PropertyManagerSponsor,
                SupportingDocs = dto.SupportingDocs ?? new List<string>(),
                Reason = dto.Reason,
                Status = dto.Status,
                ReviewDate = dto.ReviewDate,
                Reviewer = dto.Reviewer,
                Comments = dto.Comments,
                Restrictions = dto.Restrictions
            };
        }

        private static AgencyApprovalHistoryEntity MapApprovalHistoryDtoToEntity(AgencyApprovalHistoryDto dto)
        {
            return new AgencyApprovalHistoryEntity
            {
                Id = string.IsNullOrWhiteSpace(dto.Id) ? Guid.NewGuid().ToString("N") : dto.Id,
                Timestamp = dto.Timestamp,
                Type = dto.Type,
                Decision = dto.Decision,
                Reviewer = dto.Reviewer,
                Comments = dto.Comments,
                ChangesRequested = dto.ChangesRequested
            };
        }

        public static AgencyDetailResponse MapToDetailResponse(AgencyEntity entity)
        {
            return new AgencyDetailResponse
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
                UpdatedAt = entity.UpdatedAt,
                Agent = entity.Agent == null ? null : new AgencyAgentDto
                {
                    FirstName = entity.Agent.FirstName,
                    LastName = entity.Agent.LastName,
                    ProfilePhoto = entity.Agent.ProfilePhoto,
                    Designation = entity.Agent.Designation,
                    Phone = entity.Agent.Phone,
                    AlternatePhone = entity.Agent.AlternatePhone,
                    Email = entity.Agent.Email,
                    PreferredLanguage = entity.Agent.PreferredLanguage,
                    AgentType = entity.Agent.AgentType,
                    EmployeeId = entity.Agent.EmployeeId,
                    EmergencyContactName = entity.Agent.EmergencyContactName,
                    EmergencyContactNumber = entity.Agent.EmergencyContactNumber
                },
                Company = entity.Company == null ? null : new AgencyCompanyDto
                {
                    Name = entity.Company.Name,
                    RegisteredCompanyName = entity.Company.RegisteredCompanyName,
                    BusinessType = entity.Company.BusinessType,
                    RegistrationNumber = entity.Company.RegistrationNumber,
                    Gstin = entity.Company.Gstin,
                    Pan = entity.Company.Pan,
                    Website = entity.Company.Website,
                    YearOfEstablishment = entity.Company.YearOfEstablishment,
                    NumberOfEmployees = entity.Company.NumberOfEmployees,
                    CompanyEmail = entity.Company.CompanyEmail,
                    CompanyPhone = entity.Company.CompanyPhone,
                    AddressLine1 = entity.Company.AddressLine1,
                    AddressLine2 = entity.Company.AddressLine2,
                    Country = entity.Company.Country,
                    State = entity.Company.State,
                    City = entity.Company.City,
                    PinCode = entity.Company.PinCode,
                    Description = entity.Company.Description,
                    AuthorizedSignatoryName = entity.Company.AuthorizedSignatoryName,
                    AuthorizedSignatoryDesignation = entity.Company.AuthorizedSignatoryDesignation
                },
                Contacts = entity.Contacts == null ? null : new AgencyContactsDto
                {
                    PrimaryContactName = entity.Contacts.PrimaryContactName,
                    PrimaryContactDesignation = entity.Contacts.PrimaryContactDesignation,
                    PrimaryContactMobile = entity.Contacts.PrimaryContactMobile,
                    PrimaryContactEmail = entity.Contacts.PrimaryContactEmail,
                    AlternateContactPerson = entity.Contacts.AlternateContactPerson,
                    AlternateContactMobile = entity.Contacts.AlternateContactMobile,
                    AccountsBillingName = entity.Contacts.AccountsBillingName,
                    AccountsBillingEmail = entity.Contacts.AccountsBillingEmail,
                    AccountsBillingPhone = entity.Contacts.AccountsBillingPhone,
                    PreferredContactMethod = entity.Contacts.PreferredContactMethod,
                    PreferredContactTime = entity.Contacts.PreferredContactTime
                },
                Personnel = entity.Personnel.Select(p => new AgencyPersonnelDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Role = p.Role,
                    Experience = p.Experience,
                    Status = p.Status,
                    CurrentShift = p.CurrentShift,
                    Phone = p.Phone
                }).ToList(),
                Assignments = entity.Assignments.Select(a => new AgencyAssignmentDto
                {
                    Id = a.Id,
                    Property = a.Property,
                    Location = a.Location,
                    Type = a.Type,
                    Personnel = a.Personnel ?? new List<string>(),
                    Shift = a.Shift,
                    Date = a.Date,
                    Status = a.Status
                }).ToList(),
                Services = entity.Services.Select(s => new AgencyServiceDto
                {
                    Id = s.Id,
                    Category = s.Category,
                    Specialization = s.Specialization,
                    Description = s.Description,
                    OtherServiceName = s.OtherServiceName,
                    CoverageArea = s.CoverageArea,
                    ExperienceYears = s.ExperienceYears,
                    Certifications = s.Certifications,
                    Availability = s.Availability
                }).ToList(),
                Documents = entity.Documents.Select(d => new AgencyDocumentDto
                {
                    Id = d.Id,
                    DocumentType = d.DocumentType,
                    FileName = d.FileName,
                    FileUrl = d.FileUrl,
                    UploadDate = d.UploadDate,
                    ExpiryDate = d.ExpiryDate,
                    VerificationStatus = d.VerificationStatus,
                    Remarks = d.Remarks
                }).ToList(),
                PropertyAssignments = entity.PropertyAssignments.Select(pa => new AgencyPropertyAssignmentDto
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
                }).ToList(),
                ApprovalHistory = entity.ApprovalHistory.Select(ah => new AgencyApprovalHistoryDto
                {
                    Id = ah.Id,
                    Timestamp = ah.Timestamp,
                    Type = ah.Type,
                    Decision = ah.Decision,
                    Reviewer = ah.Reviewer,
                    Comments = ah.Comments,
                    ChangesRequested = ah.ChangesRequested
                }).ToList()
            };
        }
    }
}
