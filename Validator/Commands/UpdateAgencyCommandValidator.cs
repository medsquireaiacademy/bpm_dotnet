using FluentValidation;
using beeproperties.admin.webapi.models.Models.Commands;

namespace beeproperties.admin.webapi.Validator.Commands
{
    public class UpdateAgencyCommandValidator : AbstractValidator<UpdateAgencyCommand>
    {
        public UpdateAgencyCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Agency Id is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Agency Name is required.")
                .MaximumLength(200).WithMessage("Agency Name cannot exceed 200 characters.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Primary Category is required.")
                .MaximumLength(100).WithMessage("Category cannot exceed 100 characters.");

            RuleFor(x => x.ContactPerson)
                .NotEmpty().WithMessage("Contact Person is required.")
                .MaximumLength(150).WithMessage("Contact Person cannot exceed 150 characters.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .MaximumLength(30).WithMessage("Phone cannot exceed 30 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid Email address is required.")
                .MaximumLength(150).WithMessage("Email cannot exceed 150 characters.");
        }
    }
}

