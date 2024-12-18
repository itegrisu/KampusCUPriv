using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.VehicleManagementFeatures.Tyres.Queries.GetByGid
{
    public class GetByGidVehicleTyreResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidTyreTypeFK { get; set; }
        public string TyreTypeFKTitle { get; set; }
        public string TyreNo { get; set; }
        public int ProductionYear { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public EnumTyreStatus TyreStatus { get; set; }
        public string? Description { get; set; }

    }
}