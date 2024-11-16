using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleTyreUses.Commands.Create;

public class CreateVehicleTyreUseCommandValidator : AbstractValidator<CreateVehicleTyreUseCommand>
{
    public CreateVehicleTyreUseCommandValidator()
    {
        RuleFor(c => c.GidVehicleFK).NotNull().NotEmpty();
RuleFor(c => c.GidTyreFK).NotNull().NotEmpty();

RuleFor(c => c.InstallationDate).NotNull().NotEmpty();


    }
}