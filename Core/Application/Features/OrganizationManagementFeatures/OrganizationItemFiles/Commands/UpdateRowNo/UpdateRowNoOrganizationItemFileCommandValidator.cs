using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.UpdateRowNo;
using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.Create;

public class UpdateRowNoOrganizationItemFileCommandValidator : AbstractValidator<UpdateRowNoOrganizationItemFileCommand>
{
    public UpdateRowNoOrganizationItemFileCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.IsUp).NotNull().NotEmpty();
    }
}