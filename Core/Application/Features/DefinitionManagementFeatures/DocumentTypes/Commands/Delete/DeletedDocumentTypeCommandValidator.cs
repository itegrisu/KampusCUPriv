using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.DocumentTypes.Commands.Delete;

public class DeleteDocumentTypeCommandValidator : AbstractValidator<DeleteDocumentTypeCommand>
{
    public DeleteDocumentTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}