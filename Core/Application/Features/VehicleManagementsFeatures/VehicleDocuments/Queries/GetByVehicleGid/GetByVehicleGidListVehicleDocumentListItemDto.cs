using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleDocuments.Queries.GetByVehicleGid
{
    public class GetByVehicleGidListVehicleDocumentListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidVehicleFK { get; set; }
        public string VehicleAllFKPlateNumber { get; set; }
        public Guid GidDocumentType { get; set; }
        public string DocumentTypeFKName { get; set; }
        public string DocumentName { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime? DocumentLastDate { get; set; }
        public string? DocumentFile { get; set; }
        public string? Description { get; set; }
    }
}
