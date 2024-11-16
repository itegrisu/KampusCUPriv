using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleDocuments.Commands.Update;

public class UpdateVehicleDocumentCommandValidator : AbstractValidator<UpdateVehicleDocumentCommand>
{
    public UpdateVehicleDocumentCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidVehicleFK).NotNull().NotEmpty();
RuleFor(c => c.GidDocumentType).NotNull().NotEmpty();

RuleFor(c => c.DocumentName).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.DocumentDate).NotNull().NotEmpty();
RuleFor(c => c.DocumentFile).MaximumLength(150);
RuleFor(c => c.Description).MaximumLength(250);


    }
}