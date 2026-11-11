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
