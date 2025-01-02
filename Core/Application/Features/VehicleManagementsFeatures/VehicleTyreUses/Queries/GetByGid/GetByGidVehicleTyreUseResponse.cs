using Core.Application.Responses;

namespace Application.Features.VehicleManagementFeatures.VehicleTyreUses.Queries.GetByGid
{
    public class GetByGidVehicleTyreUseResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidVehicleFK { get; set; }
        public string VehicleAllFKPlateNumber { get; set; }
        public Guid GidTyreFK { get; set; }
        public string TyreFKTyreTypeFKTitle { get; set; }
        public DateTime InstallationDate { get; set; }
        public DateTime? TyreRemovalDate { get; set; }

    }
}