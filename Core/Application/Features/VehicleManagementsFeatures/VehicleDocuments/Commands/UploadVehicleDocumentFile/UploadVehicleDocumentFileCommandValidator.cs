using Application.Features.VehicleManagementsFeatures.VehicleDocuments.Commands.UploadDocumentFile;
using FluentValidation;

namespace Application.Features.VehicleManagementsFeatures.VehicleDocuments.Commands.UploadVehicleDocumentFile
{
    public class UploadVehicleDocumentFileCommandValidator : AbstractValidator<UploadVehicleDocumentFileCommand>
    {
        public UploadVehicleDocumentFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
