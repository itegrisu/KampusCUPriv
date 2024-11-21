using FluentValidation;

namespace Application.Features.TransportationManagementFeatures.Transportations.Commands.Delete;

public class DeleteTransportationCommandValidator : AbstractValidator<DeleteTransportationCommand>
{
    public DeleteTransportationCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}