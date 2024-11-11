using Application.Features.VehicleManagementsFeatures.VehicleTransactions.Commands.UploadLicenseFile;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleTransactions.Commands.UploadLicenseFile
{
    public class UploadVehicleTransactionLicenseFileCommandValidator : AbstractValidator<UploadVehicleTransactionLicenseFileCommand>
    {
        public UploadVehicleTransactionLicenseFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
