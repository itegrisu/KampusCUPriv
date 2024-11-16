using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.VehicleManagementFeatures.VehicleInspections.Queries.GetList;

public class GetListVehicleInspectionListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidVehicleFK { get; set; }
    public string VehicleAllFKPlateNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? DocumentFile { get; set; }
    public string? Description { get; set; }

}