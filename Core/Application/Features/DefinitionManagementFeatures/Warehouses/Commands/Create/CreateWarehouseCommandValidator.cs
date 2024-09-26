using FluentValidation;

namespace Application.Features.DefinationManagementFeatures.Warehouses.Commands.Create;

public class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
{
    public CreateWarehouseCommandValidator()
    {
        
RuleFor(c => c.WarehouseName).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.Location).NotNull().NotEmpty().MaximumLength(150);


    }
}