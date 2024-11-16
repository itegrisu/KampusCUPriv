using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleInsurances.Commands.UploadVehicleInsuranceFile
{
    public class UploadVehicleInsuranceFileCommandValidator : AbstractValidator<UploadVehicleInsuranceFileCommand>
    {
        public UploadVehicleInsuranceFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
