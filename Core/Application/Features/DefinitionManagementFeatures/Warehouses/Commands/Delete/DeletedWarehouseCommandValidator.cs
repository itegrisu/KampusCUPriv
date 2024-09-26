using FluentValidation;

namespace Application.Features.DefinationManagementFeatures.Warehouses.Commands.Delete;

public class DeleteWarehouseCommandValidator : AbstractValidator<DeleteWarehouseCommand>
{
    public DeleteWarehouseCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}