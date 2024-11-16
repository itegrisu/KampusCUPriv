using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.Tyres.Commands.Create;

public class CreateTyreCommandValidator : AbstractValidator<CreateTyreCommand>
{
    public CreateTyreCommandValidator()
    {
        RuleFor(c => c.GidTyreTypeFK).NotNull().NotEmpty();

RuleFor(c => c.TyreNo).NotNull().NotEmpty().MaximumLength(20);
RuleFor(c => c.TyreStatus).NotNull().NotEmpty();
RuleFor(c => c.Description).MaximumLength(250);


    }
}