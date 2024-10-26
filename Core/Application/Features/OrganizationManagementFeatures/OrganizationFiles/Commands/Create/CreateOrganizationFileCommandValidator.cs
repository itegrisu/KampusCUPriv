using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.Create;

public class CreateOrganizationFileCommandValidator : AbstractValidator<CreateOrganizationFileCommand>
{
    public CreateOrganizationFileCommandValidator()
    {
        RuleFor(c => c.GidOrganizationFK).NotNull().NotEmpty();

RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.Document).MaximumLength(150);
RuleFor(c => c.Description).MaximumLength(300);
RuleFor(c => c.RowNo).NotNull().NotEmpty();


    }
}