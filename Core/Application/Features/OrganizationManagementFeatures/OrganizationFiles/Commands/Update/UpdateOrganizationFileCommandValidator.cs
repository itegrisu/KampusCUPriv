using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.Update;

public class UpdateOrganizationFileCommandValidator : AbstractValidator<UpdateOrganizationFileCommand>
{
    public UpdateOrganizationFileCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidOrganizationFK).NotNull().NotEmpty();

RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.Document).MaximumLength(150);
RuleFor(c => c.Description).MaximumLength(300);
RuleFor(c => c.RowNo).NotNull().NotEmpty();


    }
}