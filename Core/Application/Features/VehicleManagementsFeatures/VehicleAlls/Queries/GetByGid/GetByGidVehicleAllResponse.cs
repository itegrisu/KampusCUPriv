using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.VehicleManagementFeatures.VehicleAlls.Queries.GetByGid
{
    public class GetByGidVehicleAllResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidVehicleBrand { get; set; }
        public string OtoBrandFKName { get; set; }
        public string PlateNumber { get; set; }
        public EnumVehicleType VehicleType { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public string? EngineNo { get; set; }
        public string? ChassisNumber { get; set; }
        public int PassengerCount { get; set; }
        public EnumFuelType FuelType { get; set; }
        public string? Description { get; set; }

    }
}