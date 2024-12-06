using FluentValidation;

namespace Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Update;

public class UpdateTransportationPersonnelCommandValidator : AbstractValidator<UpdateTransportationPersonnelCommand>
{
    public UpdateTransportationPersonnelCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        //RuleFor(c => c.GidTransportationServiceFK);//
        RuleFor(c => c.GidStaffPersonnelFK).NotNull().NotEmpty();
        RuleFor(c => c.StaffType).NotNull().NotEmpty();
        RuleFor(c => c.StaffStatus).NotNull().NotEmpty();
        RuleFor(c => c.Description).MaximumLength(250);
    }
}