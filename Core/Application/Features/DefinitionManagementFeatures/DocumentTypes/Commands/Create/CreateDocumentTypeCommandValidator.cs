using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.DocumentTypes.Commands.Create;

public class CreateDocumentTypeCommandValidator : AbstractValidator<CreateDocumentTypeCommand>
{
    public CreateDocumentTypeCommandValidator()
    {

        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);


    }
}