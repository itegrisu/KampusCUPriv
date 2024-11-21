using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Districts.Commands.Create;

public class CreateDistrictCommandValidator : AbstractValidator<CreateDistrictCommand>
{
    public CreateDistrictCommandValidator()
    {
        RuleFor(c => c.GidCityFK).NotNull().NotEmpty();

RuleFor(c => c.DistrictCode).NotNull().NotEmpty();
RuleFor(c => c.DistrictName).NotNull().NotEmpty().MaximumLength(50);


    }
}