using FluentValidation;

namespace Application.Features.TransportationManagementFeatures.Transportations.Commands.Update;

public class UpdateTransportationCommandValidator : AbstractValidator<UpdateTransportationCommand>
{
    public UpdateTransportationCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        //RuleFor(c => c.GidOrganizationFK);//

RuleFor(c => c.CustomerInfo).NotNull().NotEmpty().MaximumLength(150);
RuleFor(c => c.TransportationNo).NotNull().NotEmpty().MaximumLength(20);
RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.StartDate).NotNull().NotEmpty();
RuleFor(c => c.Fee).NotNull().NotEmpty();
RuleFor(c => c.TransportationStatus).NotNull().NotEmpty();


    }
}