using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Cities.Commands.Update;

public class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
{
    public UpdateCityCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidCountryFK).NotNull().NotEmpty();

        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.PlateCode).MaximumLength(5);


    }
}