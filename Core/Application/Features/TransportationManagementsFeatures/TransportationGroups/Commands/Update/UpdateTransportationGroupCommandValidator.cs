using FluentValidation;

namespace Application.Features.TransportationManagementFeatures.TransportationGroups.Commands.Update;

public class UpdateTransportationGroupCommandValidator : AbstractValidator<UpdateTransportationGroupCommand>
{
    public UpdateTransportationGroupCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidTransportationServiceFK).NotNull().NotEmpty();
        RuleFor(c => c.GidStartCountryFK).NotNull().NotEmpty();
        RuleFor(c => c.GidStartCityFK).NotNull().NotEmpty();
        RuleFor(c => c.GidStartDistrictFK).NotNull().NotEmpty();
        RuleFor(c => c.GidEndCountryFK).NotNull().NotEmpty();
        RuleFor(c => c.GidEndCityFK).NotNull().NotEmpty();
        RuleFor(c => c.GidEndDistrictFK).NotNull().NotEmpty();

        RuleFor(c => c.GroupName).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.TransportationFee).NotNull().NotEmpty();
        RuleFor(c => c.StartPlace).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(c => c.EndPlace).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(c => c.Description).MaximumLength(250);
    }
}