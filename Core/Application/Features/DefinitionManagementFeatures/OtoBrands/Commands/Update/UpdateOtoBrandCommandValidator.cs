using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.OtoBrands.Commands.Update;

public class UpdateOtoBrandCommandValidator : AbstractValidator<UpdateOtoBrandCommand>
{
    public UpdateOtoBrandCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.AracMarkaAdi).NotNull().NotEmpty().MaximumLength(100);


    }
}