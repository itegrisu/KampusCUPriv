using Core.Application.Responses;

namespace Application.Features.VehicleManagementFeatures.VehicleMaintenances.Queries.GetByGid
{
    public class GetByGidVehicleMaintenanceResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidVehicleFK { get; set; }
        public string VehicleAllFKPlateNumber { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string? ResponsiblePerson { get; set; }
        public int MaintenanceFee { get; set; }
        public string? DocumentFile { get; set; }
        public string? Description { get; set; }
        public int MaintenanceScore { get; set; }

    }
}