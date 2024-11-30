using Core.Application.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Queries.GetByTransportationGid
{
    public class GetByTransportationGidListTransportationServiceListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidTransportationFK { get; set; }
        public string TransportationFKTitle { get; set; }
        public Guid GidVehicleFK { get; set; }
        public string VehicleAllFKPlateNumber { get; set; }
        public string ServiceNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StartKM { get; set; }
        public int EndKM { get; set; }
        public string? VehiclePhone { get; set; }
        public EnumTransportationServiceStatus TransportationServiceStatus { get; set; }
        public string? TransportationFile { get; set; }
        public string? Description { get; set; }
    }
}
