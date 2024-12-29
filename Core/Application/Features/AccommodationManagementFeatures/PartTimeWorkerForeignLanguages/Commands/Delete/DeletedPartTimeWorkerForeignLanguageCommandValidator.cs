using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Commands.Delete;

public class DeletePartTimeWorkerForeignLanguageCommandValidator : AbstractValidator<DeletePartTimeWorkerForeignLanguageCommand>
{
    public DeletePartTimeWorkerForeignLanguageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}