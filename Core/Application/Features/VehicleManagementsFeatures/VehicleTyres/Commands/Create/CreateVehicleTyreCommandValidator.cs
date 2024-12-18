using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.Tyres.Commands.Create;

public class CreateVehicleTyreCommandValidator : AbstractValidator<CreateVehicleTyreCommand>
{
    public CreateVehicleTyreCommandValidator()
    {
        RuleFor(c => c.GidTyreTypeFK).NotNull().NotEmpty();

RuleFor(c => c.TyreNo).NotNull().NotEmpty().MaximumLength(20);
RuleFor(c => c.TyreStatus).NotNull().NotEmpty();
RuleFor(c => c.Description).MaximumLength(250);


    }
}