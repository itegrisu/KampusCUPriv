using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Cities.Commands.Create;

public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
{
    public CreateCityCommandValidator()
    {
        RuleFor(c => c.GidCountryFK).NotNull().NotEmpty();

        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.PlateCode).MaximumLength(5);


    }
}