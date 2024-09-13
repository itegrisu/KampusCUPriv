using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.OtoBrands.Commands.Delete;

public class DeleteOtoBrandCommandValidator : AbstractValidator<DeleteOtoBrandCommand>
{
    public DeleteOtoBrandCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}