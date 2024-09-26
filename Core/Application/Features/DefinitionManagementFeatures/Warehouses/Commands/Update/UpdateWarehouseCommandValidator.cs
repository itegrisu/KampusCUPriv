using FluentValidation;

namespace Application.Features.DefinationManagementFeatures.Warehouses.Commands.Update;

public class UpdateWarehouseCommandValidator : AbstractValidator<UpdateWarehouseCommand>
{
    public UpdateWarehouseCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.WarehouseName).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.Location).NotNull().NotEmpty().MaximumLength(150);


    }
}