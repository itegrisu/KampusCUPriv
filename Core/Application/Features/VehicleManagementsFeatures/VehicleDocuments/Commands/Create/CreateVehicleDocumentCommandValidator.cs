using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleDocuments.Commands.Create;

public class CreateVehicleDocumentCommandValidator : AbstractValidator<CreateVehicleDocumentCommand>
{
    public CreateVehicleDocumentCommandValidator()
    {
        RuleFor(c => c.GidVehicleFK).NotNull().NotEmpty();
        RuleFor(c => c.GidDocumentType).NotNull().NotEmpty();

        RuleFor(c => c.DocumentName).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.DocumentDate).NotNull().NotEmpty();
        RuleFor(c => c.DocumentFile).MaximumLength(150);
        RuleFor(c => c.Description).MaximumLength(250);


    }
}