using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Commands.Create;

public class CreatePersonnelWorkingTableCommandValidator : AbstractValidator<CreatePersonnelWorkingTableCommand>
{
    public CreatePersonnelWorkingTableCommandValidator()
    {
        RuleFor(c => c.GidPersonnelFK).NotNull().NotEmpty();

        RuleFor(c => c.StartDate).NotNull().NotEmpty();


    }
}