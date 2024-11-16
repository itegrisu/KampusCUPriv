using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleEquipments.Commands.UploadVehicleEquipmentFile
{
    public class UploadVehicleEquipmentFileCommand : IRequest<UploadVehicleEquipmentFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
}
