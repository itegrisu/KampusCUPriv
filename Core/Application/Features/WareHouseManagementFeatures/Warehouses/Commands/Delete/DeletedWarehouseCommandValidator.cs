using FluentValidation;

namespace Application.Features.WarehouseManagementFeatures.Warehouses.Commands.Delete;

public class DeleteWarehouseCommandValidator : AbstractValidator<DeleteWarehouseCommand>
{
    public DeleteWarehouseCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}