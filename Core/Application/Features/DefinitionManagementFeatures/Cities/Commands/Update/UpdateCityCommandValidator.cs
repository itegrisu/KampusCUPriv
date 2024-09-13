using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Cities.Commands.Update;

public class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
{
    public UpdateCityCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidUlkeFK).NotNull().NotEmpty();

RuleFor(c => c.SehirAdi).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.PlakaKodu).MaximumLength(5);


    }
}