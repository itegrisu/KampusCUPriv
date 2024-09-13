using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.Delete;

public class DeletePersonnelPermitInfoCommandValidator : AbstractValidator<DeletePersonnelPermitInfoCommand>
{
    public DeletePersonnelPermitInfoCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}