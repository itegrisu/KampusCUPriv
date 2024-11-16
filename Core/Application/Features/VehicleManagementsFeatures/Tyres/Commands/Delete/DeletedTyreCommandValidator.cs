using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.Tyres.Commands.Delete;

public class DeleteTyreCommandValidator : AbstractValidator<DeleteTyreCommand>
{
    public DeleteTyreCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}