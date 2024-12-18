using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleAccidents.Commands.UploadVehicleAccidentFile
{
    public class UploadVehicleAccidentFileCommand : IRequest<UploadVehicleAccidentFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
}
