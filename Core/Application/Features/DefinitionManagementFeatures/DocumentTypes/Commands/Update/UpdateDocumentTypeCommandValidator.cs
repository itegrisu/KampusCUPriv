using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.DocumentTypes.Commands.Update;

public class UpdateDocumentTypeCommandValidator : AbstractValidator<UpdateDocumentTypeCommand>
{
    public UpdateDocumentTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);
    }
}