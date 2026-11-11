using FluentValidation;
using beeproperties.admin.webapi.Validator.Commands;

namespace beeproperties.admin.webapi.FluentValidationCollectionExtensions
{
    public static class FluentValidationCollectionExtensions
    {
        public static IServiceCollection AddAppFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateCommunityCommandValidator>();
            return services;
        }
    }
}

