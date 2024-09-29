using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Countries.Commands.Update;

public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
{
    public UpdateCountryCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();

        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.CountryCode).NotNull().NotEmpty().MaximumLength(5);
        RuleFor(c => c.PhoneCode).MaximumLength(5);


    }
}