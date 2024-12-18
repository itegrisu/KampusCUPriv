using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleAccidents.Commands.UploadVehicleAccidentFile
{
    public class UploadVehicleAccidentFileCommandValidator : AbstractValidator<UploadVehicleAccidentFileCommand>
    {
        public UploadVehicleAccidentFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
