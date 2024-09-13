using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.Create;

public class CreatePersonnelPassportInfoCommandValidator : AbstractValidator<CreatePersonnelPassportInfoCommand>
{
    public CreatePersonnelPassportInfoCommandValidator()
    {
        RuleFor(c => c.GidPersonelFK).NotNull().NotEmpty();

RuleFor(c => c.PasaportNo).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.VerilisTarihi).NotNull().NotEmpty();
RuleFor(c => c.GecerlilikTarihi).NotNull().NotEmpty();
RuleFor(c => c.Belge).MaximumLength(150);
RuleFor(c => c.Aciklama).MaximumLength(250);


    }
}