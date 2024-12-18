using Application.Features.VehicleManagementsFeatures.VehicleAccidents.Commands.UploadVehicleAccidentImageFile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleAccidents.Commands.UploadVehicleAccidentImageFile
{
    public class UploadVehicleAccidentImageFileCommand : IRequest<UploadVehicleAccidentImageFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
}
