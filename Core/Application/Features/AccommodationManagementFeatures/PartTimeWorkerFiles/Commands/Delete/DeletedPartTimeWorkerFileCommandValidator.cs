using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.Delete;

public class DeletePartTimeWorkerFileCommandValidator : AbstractValidator<DeletePartTimeWorkerFileCommand>
{
    public DeletePartTimeWorkerFileCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}