using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.JobTypes.Commands.Create;

public class CreateJobTypeCommandValidator : AbstractValidator<CreateJobTypeCommand>
{
    public CreateJobTypeCommandValidator()
    {
        
RuleFor(c => c.GorevAdi).NotNull().NotEmpty().MaximumLength(100);


    }
}