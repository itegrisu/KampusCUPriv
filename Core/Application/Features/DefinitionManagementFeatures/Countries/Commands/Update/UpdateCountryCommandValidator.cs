using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Countries.Commands.Update;

public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
{
    public UpdateCountryCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.UlkeAdi).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.UlkeKodu).NotNull().NotEmpty().MaximumLength(5);
        RuleFor(c => c.TelefonKodu).MaximumLength(5);


    }
}