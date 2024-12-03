using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetList;

public class GetListTransportationServiceListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidTransportationFK { get; set; }
    public string TransportationFKTitle { get; set; }
    public Guid GidVehicleFK { get; set; }
    public string VehicleAllFKPlateNumber { get; set; }
    public string ServiceNo { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StartKM { get; set; }
    public int EndKM { get; set; }
    public string? VehiclePhone { get; set; }
    public EnumTransportationServiceStatus TransportationServiceStatus { get; set; }
    public string? TransportationFile { get; set; }
    public string? Description { get; set; }
    public string? RefNoTransportation { get; set; }
}