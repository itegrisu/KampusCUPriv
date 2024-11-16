using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleEquipments.Commands.UploadVehicleEquipmentFile
{
    public class UploadVehicleEquipmentFileCommandValidator : AbstractValidator<UploadVehicleEquipmentFileCommand>
    {
        public UploadVehicleEquipmentFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
