using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Commands.Update;

public class UpdatePersonnelWorkingTableCommandValidator : AbstractValidator<UpdatePersonnelWorkingTableCommand>
{
    public UpdatePersonnelWorkingTableCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidPersonnelFK).NotNull().NotEmpty();
        RuleFor(c => c.StartDate).NotNull().NotEmpty();


    }
}