using Core.Application.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.Tyres.Queries.GetByVehicleGid
{
    public class GetByVehicleGidListVehicleTyreListItemDto :IDto
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
