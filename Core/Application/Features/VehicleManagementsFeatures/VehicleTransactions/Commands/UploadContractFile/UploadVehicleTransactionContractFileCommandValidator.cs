using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleTransactions.Commands.UploadContractFile
{
    public class UploadVehicleTransactionContractFileCommandValidator : AbstractValidator<UploadVehicleTransactionContractFileCommand>
    {
        public UploadVehicleTransactionContractFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
