using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.JobTypes.Commands.Update;

public class UpdateJobTypeCommandValidator : AbstractValidator<UpdateJobTypeCommand>
{
    public UpdateJobTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.GorevAdi).NotNull().NotEmpty().MaximumLength(100);


    }
}