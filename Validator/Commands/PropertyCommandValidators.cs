using beeproperties.admin.webapi.models.Models.Commands;
using FluentValidation;

namespace beeproperties.admin.webapi.Validator.Commands
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Property name is required.")
                .MaximumLength(200).WithMessage("Property name cannot exceed 200 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State is required.");
        }
    }

    public class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
    {
        public UpdatePropertyCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Property ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Property name is required.");
        }
    }

    public class CreateBlockCommandValidator : AbstractValidator<CreateBlockCommand>
    {
        public CreateBlockCommandValidator()
        {
            RuleFor(x => x.PropertyId)
                .NotEmpty().WithMessage("Property ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Block name is required.");
        }
    }

    public class CreateFlatCommandValidator : AbstractValidator<CreateFlatCommand>
    {
        public CreateFlatCommandValidator()
        {
            RuleFor(x => x.PropertyId)
                .NotEmpty().WithMessage("Property ID is required.");

            RuleFor(x => x.BlockId)
                .NotEmpty().WithMessage("Block ID is required.");

            RuleFor(x => x.FloorId)
                .NotEmpty().WithMessage("Floor ID is required.");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Flat number is required.");
        }
    }
}
