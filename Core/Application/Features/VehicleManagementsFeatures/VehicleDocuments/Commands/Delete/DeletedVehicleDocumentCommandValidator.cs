using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleDocuments.Commands.Delete;

public class DeleteVehicleDocumentCommandValidator : AbstractValidator<DeleteVehicleDocumentCommand>
{
    public DeleteVehicleDocumentCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}