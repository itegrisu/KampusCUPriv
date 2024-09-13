using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.Delete;

public class DeletePersonnelResidenceInfoCommandValidator : AbstractValidator<DeletePersonnelResidenceInfoCommand>
{
    public DeletePersonnelResidenceInfoCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}