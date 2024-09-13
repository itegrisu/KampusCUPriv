using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.Create;

public class CreatePersonnelPermitInfoCommandValidator : AbstractValidator<CreatePersonnelPermitInfoCommand>
{
    public CreatePersonnelPermitInfoCommandValidator()
    {
        RuleFor(c => c.GidPersonelFK).NotNull().NotEmpty();
RuleFor(c => c.GidPermitFK).NotNull().NotEmpty();

RuleFor(c => c.IzinBaslamaTarihi).NotNull().NotEmpty();
RuleFor(c => c.IzinBitisTarihi).NotNull().NotEmpty();
RuleFor(c => c.Belge).MaximumLength(150);
RuleFor(c => c.Aciklama).MaximumLength(250);


    }
}