using FluentValidation;

namespace Application.Features.LogManagementFeatures.LogUserPageVisits.Commands.Delete;

public class DeleteLogUserPageVisitCommandValidator : AbstractValidator<DeleteLogUserPageVisitCommand>
{
    public DeleteLogUserPageVisitCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}