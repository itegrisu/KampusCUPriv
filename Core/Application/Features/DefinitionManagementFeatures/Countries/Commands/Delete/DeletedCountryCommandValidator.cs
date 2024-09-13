using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Countries.Commands.Delete;

public class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
{
    public DeleteCountryCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}