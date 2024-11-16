using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleMaintenances.Commands.UploadVehicleMaintenanceFile
{
    public class UploadVehicleMaintenanceFileCommand : IRequest<UploadVehicleMaintenanceFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
}
