using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Commands.Delete;

public class DeletePartTimeWorkerCommandValidator : AbstractValidator<DeletePartTimeWorkerCommand>
{
    public DeletePartTimeWorkerCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}