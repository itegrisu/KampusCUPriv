using FluentValidation;

namespace Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Delete;

public class DeleteTransportationPersonnelCommandValidator : AbstractValidator<DeleteTransportationPersonnelCommand>
{
    public DeleteTransportationPersonnelCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}