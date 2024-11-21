using FluentValidation;

namespace Application.Features.TransportationManagementFeatures.TransportationGroups.Commands.Delete;

public class DeleteTransportationGroupCommandValidator : AbstractValidator<DeleteTransportationGroupCommand>
{
    public DeleteTransportationGroupCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}