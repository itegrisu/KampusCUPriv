using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.Tyres.Commands.Update;

public class UpdateVehicleTyreCommandValidator : AbstractValidator<UpdateVehicleTyreCommand>
{
    public UpdateVehicleTyreCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidTyreTypeFK).NotNull().NotEmpty();

RuleFor(c => c.TyreNo).NotNull().NotEmpty().MaximumLength(20);
RuleFor(c => c.TyreStatus).NotNull().NotEmpty();
RuleFor(c => c.Description).MaximumLength(250);


    }
}