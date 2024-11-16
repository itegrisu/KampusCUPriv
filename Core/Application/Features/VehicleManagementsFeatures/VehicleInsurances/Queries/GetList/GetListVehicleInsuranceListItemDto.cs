using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.VehicleManagementFeatures.VehicleInsurances.Queries.GetList;

public class GetListVehicleInsuranceListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidVehicleFK { get; set; }
    public string VehicleAllFKPlateNumber { get; set; }
    public EnumInsuranceType InsuranceType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int InsuranceFee { get; set; }
    public string? DocumentFile { get; set; }
    public string? Description { get; set; }

}