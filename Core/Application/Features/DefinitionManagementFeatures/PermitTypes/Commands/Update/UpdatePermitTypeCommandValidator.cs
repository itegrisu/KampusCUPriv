using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.PermitTypes.Commands.Update;

public class UpdatePermitTypeCommandValidator : AbstractValidator<UpdatePermitTypeCommand>
{
    public UpdatePermitTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.IzinAdi).NotNull().NotEmpty().MaximumLength(100);


    }
}