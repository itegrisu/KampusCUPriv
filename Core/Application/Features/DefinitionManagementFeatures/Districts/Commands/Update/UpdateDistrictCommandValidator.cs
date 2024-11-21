using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Districts.Commands.Update;

public class UpdateDistrictCommandValidator : AbstractValidator<UpdateDistrictCommand>
{
    public UpdateDistrictCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidCityFK).NotNull().NotEmpty();

RuleFor(c => c.DistrictCode).NotNull().NotEmpty();
RuleFor(c => c.DistrictName).NotNull().NotEmpty().MaximumLength(50);


    }
}