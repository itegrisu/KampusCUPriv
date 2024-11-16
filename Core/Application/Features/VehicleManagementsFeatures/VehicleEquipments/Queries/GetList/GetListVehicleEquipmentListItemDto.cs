using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.VehicleManagementFeatures.VehicleEquipments.Queries.GetList;

public class GetListVehicleEquipmentListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidVehicleFK { get; set; }
    public string VehicleAllFKPlateNumber { get; set; }
    public string EquipmentName { get; set; }
    public string? DocumentFile { get; set; }
    public string? Description { get; set; }

}