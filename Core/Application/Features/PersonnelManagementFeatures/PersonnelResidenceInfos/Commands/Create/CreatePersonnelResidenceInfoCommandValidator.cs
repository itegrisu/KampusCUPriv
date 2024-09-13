using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.Create;

public class CreatePersonnelResidenceInfoCommandValidator : AbstractValidator<CreatePersonnelResidenceInfoCommand>
{
    public CreatePersonnelResidenceInfoCommandValidator()
    {
        RuleFor(c => c.GidPersonelFK).NotNull().NotEmpty();

RuleFor(c => c.OturumSeriNo).NotNull().NotEmpty().MaximumLength(50);
RuleFor(c => c.VerilisTarihi).NotNull().NotEmpty();
RuleFor(c => c.GecerlilikTarihi).NotNull().NotEmpty();
RuleFor(c => c.Belge).MaximumLength(150);
RuleFor(c => c.Aciklama).MaximumLength(250);


    }
}