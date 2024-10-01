using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.Create;

public class CreateOrganizationItemCommandValidator : AbstractValidator<CreateOrganizationItemCommand>
{
    public CreateOrganizationItemCommandValidator()
    {
        RuleFor(c => c.GidOrganizationGroupFK).NotNull().NotEmpty();
        //RuleFor(c => c.GidMainResponsibleUserFK);//

        RuleFor(c => c.ItemName).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.StartDate).NotNull().NotEmpty();
        RuleFor(c => c.EndDate).NotNull().NotEmpty();
        RuleFor(c => c.Priority).NotNull().NotEmpty();
        RuleFor(c => c.IsStar).NotNull().NotEmpty();
        RuleFor(c => c.ItemStatus).NotNull().NotEmpty();


    }
}