using FluentValidation;

namespace Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Delete;

public class DeleteTransportationServiceCommandValidator : AbstractValidator<DeleteTransportationServiceCommand>
{
    public DeleteTransportationServiceCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}