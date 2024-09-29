using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.JobTypes.Commands.Update;

public class UpdateJobTypeCommandValidator : AbstractValidator<UpdateJobTypeCommand>
{
    public UpdateJobTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);


    }
}