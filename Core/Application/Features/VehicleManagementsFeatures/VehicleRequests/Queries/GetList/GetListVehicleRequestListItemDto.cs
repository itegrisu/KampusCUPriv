using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.VehicleManagementFeatures.VehicleRequests.Queries.GetList;

public class GetListVehicleRequestListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidVehicleFK { get; set; }
    public string VehicleAllFKPlateNumber { get; set; }
    public Guid GidRequestUserFK { get; set; }
    public string RequestUserFKFullName { get; set; }
    public Guid? GidApprovedUserFK { get; set; }
    public string ApprovedUserFKFullName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string UseAim { get; set; }
    public EnumVehicleApprovedStatus VehicleApprovedStatus { get; set; }


}