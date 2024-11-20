using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleRequests.Commands.Create;

public class CreateVehicleRequestCommandValidator : AbstractValidator<CreateVehicleRequestCommand>
{
    public CreateVehicleRequestCommandValidator()
    {
        RuleFor(c => c.GidVehicleFK).NotNull().NotEmpty();
RuleFor(c => c.GidRequestUserFK).NotNull().NotEmpty();
//RuleFor(c => c.GidApprovedUserFK);//

RuleFor(c => c.StartDate).NotNull().NotEmpty();
RuleFor(c => c.EndDate).NotNull().NotEmpty();
RuleFor(c => c.UseAim).NotNull().NotEmpty().MaximumLength(250);
RuleFor(c => c.VehicleApprovedStatus).NotNull().NotEmpty();


    }
}