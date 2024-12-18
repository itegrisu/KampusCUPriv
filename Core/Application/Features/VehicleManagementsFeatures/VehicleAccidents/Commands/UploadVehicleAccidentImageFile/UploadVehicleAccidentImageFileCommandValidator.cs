using Application.Features.VehicleManagementsFeatures.VehicleAccidents.Commands.UploadVehicleAccidentImageFile;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleAccidents.Commands.UploadVehicleAccidentImageFile
{
    public class UploadVehicleAccidentImageFileCommandValidator : AbstractValidator<UploadVehicleAccidentImageFileCommand>
    {
        public UploadVehicleAccidentImageFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
