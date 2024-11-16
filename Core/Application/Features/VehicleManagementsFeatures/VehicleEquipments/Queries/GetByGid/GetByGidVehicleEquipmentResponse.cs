using Core.Application.Responses;

namespace Application.Features.VehicleManagementFeatures.VehicleEquipments.Queries.GetByGid
{
    public class GetByGidVehicleEquipmentResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidVehicleFK { get; set; }
        public string VehicleAllFKPlateNumber { get; set; }
        public string EquipmentName { get; set; }
        public string? DocumentFile { get; set; }
        public string? Description { get; set; }

    }
}