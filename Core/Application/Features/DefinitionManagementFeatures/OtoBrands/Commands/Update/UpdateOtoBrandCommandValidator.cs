using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.OtoBrands.Commands.Update;

public class UpdateOtoBrandCommandValidator : AbstractValidator<UpdateOtoBrandCommand>
{
    public UpdateOtoBrandCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);


    }
}