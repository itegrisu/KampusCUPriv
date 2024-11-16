using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleTyreUses.Commands.Update;

public class UpdateVehicleTyreUseCommandValidator : AbstractValidator<UpdateVehicleTyreUseCommand>
{
    public UpdateVehicleTyreUseCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidVehicleFK).NotNull().NotEmpty();
RuleFor(c => c.GidTyreFK).NotNull().NotEmpty();

RuleFor(c => c.InstallationDate).NotNull().NotEmpty();


    }
}