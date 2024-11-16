using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleInspections.Commands.UploadVehicleInspectionFile
{
    public class UploadVehicleInspectionFileCommandValidator : AbstractValidator<UploadVehicleInspectionFileCommand>
    {
        public UploadVehicleInspectionFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
