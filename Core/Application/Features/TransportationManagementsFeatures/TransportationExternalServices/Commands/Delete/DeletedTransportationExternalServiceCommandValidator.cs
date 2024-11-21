using FluentValidation;

namespace Application.Features.TransportationManagementFeatures.TransportationExternalServices.Commands.Delete;

public class DeleteTransportationExternalServiceCommandValidator : AbstractValidator<DeleteTransportationExternalServiceCommand>
{
    public DeleteTransportationExternalServiceCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}