using Core.Application.Responses;

namespace Application.Features.VehicleManagementFeatures.VehicleInspections.Queries.GetByGid
{
    public class GetByGidVehicleInspectionResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidVehicleFK { get; set; }
        public string VehicleAllFKPlateNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? DocumentFile { get; set; }
        public string? Description { get; set; }

    }
}