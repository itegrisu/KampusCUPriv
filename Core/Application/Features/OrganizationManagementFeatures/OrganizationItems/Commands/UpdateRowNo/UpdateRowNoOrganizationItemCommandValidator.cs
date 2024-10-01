using Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.UpdateRowNo;
using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.Create;

public class UpdateRowNoOrganizationItemCommandValidator : AbstractValidator<UpdateRowNoOrganizationItemCommand>
{
    public UpdateRowNoOrganizationItemCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.IsUp).NotNull().NotEmpty();
    }
}