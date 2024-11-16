using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.TyreTypes.Commands.Create;

public class CreateTyreTypeCommandValidator : AbstractValidator<CreateTyreTypeCommand>
{
    public CreateTyreTypeCommandValidator()
    {
        
RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(50);
RuleFor(c => c.TyreTypeName).NotNull().NotEmpty();
RuleFor(c => c.Size).MaximumLength(50);


    }
}