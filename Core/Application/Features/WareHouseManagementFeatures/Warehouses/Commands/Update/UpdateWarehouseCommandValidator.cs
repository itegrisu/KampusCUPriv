using FluentValidation;

namespace Application.Features.WarehouseManagementFeatures.Warehouses.Commands.Update;

public class UpdateWarehouseCommandValidator : AbstractValidator<UpdateWarehouseCommand>
{
    public UpdateWarehouseCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        //RuleFor(c => c.GidOrganizationFK);//

RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.WarehouseType).NotNull().NotEmpty();
RuleFor(c => c.Address).MaximumLength(250);
RuleFor(c => c.Description).MaximumLength(250);


    }
}