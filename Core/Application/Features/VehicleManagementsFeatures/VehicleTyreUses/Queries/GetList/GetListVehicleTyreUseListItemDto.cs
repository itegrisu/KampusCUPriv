using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.VehicleManagementFeatures.VehicleTyreUses.Queries.GetList;

public class GetListVehicleTyreUseListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidVehicleFK { get; set; }
    public string VehicleAllFKPlateNumber { get; set; }
    public Guid GidTyreFK { get; set; }
    public string TyreFKTyreNo { get; set; }
    public DateTime InstallationDate { get; set; }
    public DateTime? TyreRemovalDate { get; set; }
}