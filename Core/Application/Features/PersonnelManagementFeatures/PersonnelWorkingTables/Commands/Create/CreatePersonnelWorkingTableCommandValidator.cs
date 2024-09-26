using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Commands.Create;

public class CreatePersonnelWorkingTableCommandValidator : AbstractValidator<CreatePersonnelWorkingTableCommand>
{
    public CreatePersonnelWorkingTableCommandValidator()
    {
        RuleFor(c => c.GidPersonelFK).NotNull().NotEmpty();

        RuleFor(c => c.IseBaslamaTarihi).NotNull().NotEmpty();


    }
}