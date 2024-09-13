using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.JobTypes.Commands.Delete;

public class DeleteJobTypeCommandValidator : AbstractValidator<DeleteJobTypeCommand>
{
    public DeleteJobTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}