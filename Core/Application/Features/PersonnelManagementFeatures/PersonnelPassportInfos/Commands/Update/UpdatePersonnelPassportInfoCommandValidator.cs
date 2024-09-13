using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.Update;

public class UpdatePersonnelPassportInfoCommandValidator : AbstractValidator<UpdatePersonnelPassportInfoCommand>
{
    public UpdatePersonnelPassportInfoCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidPersonelFK).NotNull().NotEmpty();
        RuleFor(c => c.PasaportNo).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.VerilisTarihi).NotNull().NotEmpty();
        RuleFor(c => c.GecerlilikTarihi).NotNull().NotEmpty();
        RuleFor(c => c.Belge).MaximumLength(150);
        RuleFor(c => c.Aciklama).MaximumLength(250);


    }
}