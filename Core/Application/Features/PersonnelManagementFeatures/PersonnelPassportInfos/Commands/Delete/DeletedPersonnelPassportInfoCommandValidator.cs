using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.Delete;

public class DeletePersonnelPassportInfoCommandValidator : AbstractValidator<DeletePersonnelPassportInfoCommand>
{
    public DeletePersonnelPassportInfoCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}