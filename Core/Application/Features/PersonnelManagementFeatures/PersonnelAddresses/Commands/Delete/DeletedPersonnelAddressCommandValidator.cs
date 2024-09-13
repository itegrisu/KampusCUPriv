using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Commands.Delete;

public class DeletePersonnelAddressCommandValidator : AbstractValidator<DeletePersonnelAddressCommand>
{
    public DeletePersonnelAddressCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}