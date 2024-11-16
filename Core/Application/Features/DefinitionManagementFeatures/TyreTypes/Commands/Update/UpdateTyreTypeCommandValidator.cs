using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.TyreTypes.Commands.Update;

public class UpdateTyreTypeCommandValidator : AbstractValidator<UpdateTyreTypeCommand>
{
    public UpdateTyreTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(50);
RuleFor(c => c.TyreTypeName).NotNull().NotEmpty();
RuleFor(c => c.Size).MaximumLength(50);


    }
}