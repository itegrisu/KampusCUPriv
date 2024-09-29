using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.Update;

public class UpdatePersonnelDocumentCommandValidator : AbstractValidator<UpdatePersonnelDocumentCommand>
{
    public UpdatePersonnelDocumentCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidPersonnelFK).NotNull().NotEmpty();
        RuleFor(c => c.GidDocumentType).NotNull().NotEmpty();

        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.Document).MaximumLength(150);
        RuleFor(c => c.Description).MaximumLength(250);


    }
}