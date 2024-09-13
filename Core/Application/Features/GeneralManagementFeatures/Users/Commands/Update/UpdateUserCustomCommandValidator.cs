using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.Update;

public class UpdateUserCustomCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCustomCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        //RuleFor(c => c.GidUyrukFK);//
        RuleFor(c => c.Adi).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.Soyadi).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.EPosta).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.Unvani).MaximumLength(60);
        RuleFor(c => c.SifreGuncellemeToken).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(c => c.ProfilResmi).MaximumLength(150);
        RuleFor(c => c.AktifHesapMi).NotNull().NotEmpty();
        RuleFor(c => c.SistemAdminMi).NotNull().NotEmpty();
        RuleFor(c => c.Gsm).NotNull().NotEmpty().MaximumLength(20);
        RuleFor(c => c.DogumYeri).MaximumLength(50);
        RuleFor(c => c.KimlikNo).MaximumLength(20);
        RuleFor(c => c.PasaportNo).MaximumLength(20);
        RuleFor(c => c.SGKNo).MaximumLength(50);
        RuleFor(c => c.EhliyetNo).MaximumLength(50);
        RuleFor(c => c.Not).MaximumLength(300);
        RuleFor(c => c.Cinsiyet).NotNull().NotEmpty();
        RuleFor(c => c.EMailAktivasyonDurumu).NotNull().NotEmpty();
        RuleFor(c => c.SmsAktivasyonDurumu).NotNull().NotEmpty();
    }
}