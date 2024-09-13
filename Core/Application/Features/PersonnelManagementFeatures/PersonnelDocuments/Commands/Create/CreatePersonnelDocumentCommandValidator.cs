using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.Create;

public class CreatePersonnelDocumentCommandValidator : AbstractValidator<CreatePersonnelDocumentCommand>
{
    public CreatePersonnelDocumentCommandValidator()
    {
        RuleFor(c => c.GidPersonelFK).NotNull().NotEmpty();
        RuleFor(c => c.GidBelgeTuru).NotNull().NotEmpty();
        RuleFor(c => c.BelgeAdi).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.Belge).MaximumLength(150);
        RuleFor(c => c.Aciklama).MaximumLength(250);


    }
}