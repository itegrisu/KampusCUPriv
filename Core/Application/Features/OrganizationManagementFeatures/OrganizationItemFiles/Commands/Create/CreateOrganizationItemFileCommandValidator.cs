using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.Create;

public class CreateOrganizationItemFileCommandValidator : AbstractValidator<CreateOrganizationItemFileCommand>
{
    public CreateOrganizationItemFileCommandValidator()
    {
        RuleFor(c => c.GidOrganizationItemFK).NotNull().NotEmpty();

        RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);
        //RuleFor(c => c.Document).MaximumLength(150);
        RuleFor(c => c.Description).MaximumLength(300);
        //RuleFor(c => c.RowNo).NotNull().NotEmpty();


    }
}