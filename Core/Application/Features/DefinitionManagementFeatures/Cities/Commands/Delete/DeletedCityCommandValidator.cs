using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Cities.Commands.Delete;

public class DeleteCityCommandValidator : AbstractValidator<DeleteCityCommand>
{
    public DeleteCityCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}