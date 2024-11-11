using FluentValidation;

namespace Application.Features.WarehouseManagementFeatures.Warehouses.Commands.Create;

public class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
{
    public CreateWarehouseCommandValidator()
    {
        //RuleFor(c => c.GidOrganizationFK);//

        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.WarehouseType).NotNull().NotEmpty();
        RuleFor(c => c.Location).MaximumLength(150);
        RuleFor(c => c.Description).MaximumLength(250);


    }
}