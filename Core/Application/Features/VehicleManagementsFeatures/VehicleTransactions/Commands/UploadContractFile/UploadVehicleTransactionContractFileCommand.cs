using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleTransactions.Commands.UploadContractFile
{
    public class UploadVehicleTransactionContractFileCommand : IRequest<UploadVehicleTransactionContractFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
}
