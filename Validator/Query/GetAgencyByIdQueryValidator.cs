using FluentValidation;
using beeproperties.admin.webapi.models.Models.Query;

namespace beeproperties.admin.webapi.Validator.Query
{
    public class GetAgencyByIdQueryValidator : AbstractValidator<GetAgencyByIdQuery>
    {
        public GetAgencyByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Agency Id is required.");
        }
    }
}

