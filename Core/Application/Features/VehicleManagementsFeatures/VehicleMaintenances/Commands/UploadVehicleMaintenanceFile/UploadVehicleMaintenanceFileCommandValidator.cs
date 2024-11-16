using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleMaintenances.Commands.UploadVehicleMaintenanceFile
{
    public class UploadVehicleMaintenanceFileCommandValidator : AbstractValidator<UploadVehicleMaintenanceFileCommand>
    {
        public UploadVehicleMaintenanceFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
