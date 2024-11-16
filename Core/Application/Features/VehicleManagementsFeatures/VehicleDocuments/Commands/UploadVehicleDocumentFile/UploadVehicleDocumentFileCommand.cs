using Application.Features.VehicleManagementsFeatures.VehicleDocuments.Commands.UploadVehicleDocumentFile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleDocuments.Commands.UploadDocumentFile
{
    public class UploadVehicleDocumentFileCommand : IRequest<UploadVehicleDocumentFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
}
