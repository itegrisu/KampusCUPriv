using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.Update;

public class UpdatePersonnelGraduatedSchoolCommandValidator : AbstractValidator<UpdatePersonnelGraduatedSchoolCommand>
{
    public UpdatePersonnelGraduatedSchoolCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidPersonelFK).NotNull().NotEmpty();

RuleFor(c => c.EgitimKurumuTuru).NotNull().NotEmpty();
RuleFor(c => c.OkulBilgisi).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.BolumBilgisi).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.BaslamaYili).NotNull().NotEmpty();
RuleFor(c => c.Belge).MaximumLength(150);
RuleFor(c => c.Aciklama).MaximumLength(250);


    }
}