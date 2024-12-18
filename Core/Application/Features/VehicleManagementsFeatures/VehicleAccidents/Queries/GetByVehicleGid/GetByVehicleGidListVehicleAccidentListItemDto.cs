using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleAccidents.Queries.GetByUserGid
{
    public class GetByVehicleGidListVehicleAccidentListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidVehicleFK { get; set; }
        public string VehicleAllFKPlateNumber { get; set; }
        public DateTime AccidentDate { get; set; }
        public string Driver { get; set; }
        public string? AccidentFile { get; set; }
        public string? AccidentImageFile { get; set; }
    }
}
