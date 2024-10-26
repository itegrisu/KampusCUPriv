using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.UpdateRowNo;
using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.Create;

public class UpdateRowNoOrganizationFileCommandValidator : AbstractValidator<UpdateRowNoOrganizationFileCommand>
{
    public UpdateRowNoOrganizationFileCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.IsUp).NotNull().NotEmpty();
    }
}