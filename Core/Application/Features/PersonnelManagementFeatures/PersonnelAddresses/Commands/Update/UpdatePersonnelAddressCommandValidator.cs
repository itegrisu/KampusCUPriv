using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Commands.Update;

public class UpdatePersonnelAddressCommandValidator : AbstractValidator<UpdatePersonnelAddressCommand>
{
    public UpdatePersonnelAddressCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidPersonelFK).NotNull().NotEmpty();
RuleFor(c => c.GidSehirFK).NotNull().NotEmpty();

RuleFor(c => c.AdresBasligi).NotNull().NotEmpty().MaximumLength(50);
RuleFor(c => c.Adres).NotNull().NotEmpty().MaximumLength(150);
RuleFor(c => c.Aciklama).MaximumLength(250);


    }
}