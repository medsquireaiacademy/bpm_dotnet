using FluentValidation;
using beeproperties.admin.webapi.models.Models.Commands;

namespace beeproperties.admin.webapi.Validator.Commands
{
    public class CreateCommunityCommandValidator : AbstractValidator<CreateCommunityCommand>
    {
        public CreateCommunityCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Community Name is required.")
                .MaximumLength(200).WithMessage("Community Name cannot exceed 200 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(300).WithMessage("Address cannot exceed 300 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State is required.")
                .MaximumLength(50).WithMessage("State cannot exceed 50 characters.");

            RuleFor(x => x.Zip)
                .NotEmpty().WithMessage("Zip code is required.")
                .MaximumLength(20).WithMessage("Zip code cannot exceed 20 characters.");
        }
    }
}

