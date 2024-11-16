using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleInspections.Commands.UploadVehicleInspectionFile
{
    public class UploadVehicleInspectionFileCommand : IRequest<UploadVehicleInspectionFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
}
