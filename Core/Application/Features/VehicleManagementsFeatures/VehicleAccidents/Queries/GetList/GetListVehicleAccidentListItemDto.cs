using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.VehicleManagementFeatures.VehicleAccidents.Queries.GetList;

public class GetListVehicleAccidentListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidVehicleFK { get; set; }
    public string VehicleAllFKPlateNumber { get; set; }
    public DateTime AccidentDate { get; set; }
    public string Driver { get; set; }
    public string? AccidentFile { get; set; }
    public string? AccidentImageFile { get; set; }
}