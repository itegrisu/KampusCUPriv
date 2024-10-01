using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.Organizations.Commands.Create;

public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
{
    public CreateOrganizationCommandValidator()
    {
        RuleFor(c => c.GidCustomerFK).NotNull().NotEmpty();
RuleFor(c => c.GidResponsibleUserFK).NotNull().NotEmpty();
RuleFor(c => c.GidOrganizationTypeFK).NotNull().NotEmpty();

RuleFor(c => c.OrganizationName).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.StartDate).NotNull().NotEmpty();
RuleFor(c => c.EndDate).NotNull().NotEmpty();
RuleFor(c => c.OrganizationStatus).NotNull().NotEmpty();
RuleFor(c => c.Description).MaximumLength(250);


    }
}