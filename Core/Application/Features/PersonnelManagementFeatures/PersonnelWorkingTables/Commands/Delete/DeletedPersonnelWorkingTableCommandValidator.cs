using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Commands.Delete;

public class DeletePersonnelWorkingTableCommandValidator : AbstractValidator<DeletePersonnelWorkingTableCommand>
{
    public DeletePersonnelWorkingTableCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}