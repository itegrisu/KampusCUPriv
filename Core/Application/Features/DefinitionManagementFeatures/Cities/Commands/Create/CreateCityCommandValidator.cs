using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Cities.Commands.Create;

public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
{
    public CreateCityCommandValidator()
    {
        RuleFor(c => c.GidUlkeFK).NotNull().NotEmpty();
        RuleFor(c => c.SehirAdi).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.PlakaKodu).MaximumLength(5);


    }
}