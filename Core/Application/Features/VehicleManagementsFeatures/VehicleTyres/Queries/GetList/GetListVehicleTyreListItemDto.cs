using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.VehicleManagementFeatures.Tyres.Queries.GetList;

public class GetListVehicleTyreListItemDto : IDto
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