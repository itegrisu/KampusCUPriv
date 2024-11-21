using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Districts.Commands.Delete;

public class DeleteDistrictCommandValidator : AbstractValidator<DeleteDistrictCommand>
{
    public DeleteDistrictCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}