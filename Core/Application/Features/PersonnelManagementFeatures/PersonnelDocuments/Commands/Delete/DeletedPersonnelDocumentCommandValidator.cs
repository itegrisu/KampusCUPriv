using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.Delete;

public class DeletePersonnelDocumentCommandValidator : AbstractValidator<DeletePersonnelDocumentCommand>
{
    public DeletePersonnelDocumentCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}