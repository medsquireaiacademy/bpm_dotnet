using FluentValidation;
using beeproperties.admin.webapi.models.Models.Query;

namespace beeproperties.admin.webapi.Validator.Query
{
    public class GetCommunityByIdQueryValidator : AbstractValidator<GetCommunityByIdQuery>
    {
        public GetCommunityByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Community Id is required.");
        }
    }
}

