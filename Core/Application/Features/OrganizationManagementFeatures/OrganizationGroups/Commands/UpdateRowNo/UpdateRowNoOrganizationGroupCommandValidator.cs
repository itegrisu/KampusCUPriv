using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.UpdateRowNo;
using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.Create;

public class UpdateRowNoOrganizationGroupCommandValidator : AbstractValidator<UpdateRowNoOrganizationGroupCommand>
{
    public UpdateRowNoOrganizationGroupCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.IsUp).NotNull().NotEmpty();
    }
}