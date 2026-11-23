using beeproperties.admin.webapi.models.Models.Commands;
using FluentValidation;

namespace beeproperties.admin.webapi.Validator.Commands
{
    public class CreateMaintenanceTicketCommandValidator : AbstractValidator<CreateMaintenanceTicketCommand>
    {
        public CreateMaintenanceTicketCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(250).WithMessage("Title cannot exceed 250 characters.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.");

            RuleFor(x => x.ApartmentId)
                .NotEmpty().WithMessage("Apartment ID is required.");
        }
    }

    public class AllocateAgencyToMaintenanceCommandValidator : AbstractValidator<AllocateAgencyToMaintenanceCommand>
    {
        public AllocateAgencyToMaintenanceCommandValidator()
        {
            RuleFor(x => x.MaintenanceId)
                .NotEmpty().WithMessage("Maintenance ID is required.");

            RuleFor(x => x.AgencyId)
                .NotEmpty().WithMessage("Agency ID is required.");
        }
    }

    public class CreateSecurityDeploymentCommandValidator : AbstractValidator<CreateSecurityDeploymentCommand>
    {
        public CreateSecurityDeploymentCommandValidator()
        {
            RuleFor(x => x.ApartmentId)
                .NotEmpty().WithMessage("Apartment ID is required.");

            RuleFor(x => x.AgencyId)
                .NotEmpty().WithMessage("Agency ID is required.");

            RuleFor(x => x.Shift)
                .NotEmpty().WithMessage("Shift is required.");
        }
    }

    public class CreateServiceCategoryCommandValidator : AbstractValidator<CreateServiceCategoryCommand>
    {
        public CreateServiceCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.");
        }
    }

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username or email is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }

    public class ChangeAdminPasswordCommandValidator : AbstractValidator<ChangeAdminPasswordCommand>
    {
        public ChangeAdminPasswordCommandValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(6).WithMessage("New password must be at least 6 characters.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("Confirm password must match new password.");
        }
    }
}
