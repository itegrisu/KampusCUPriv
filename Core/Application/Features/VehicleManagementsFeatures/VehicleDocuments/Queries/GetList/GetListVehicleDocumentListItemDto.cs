using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.VehicleManagementFeatures.VehicleDocuments.Queries.GetList;

public class GetListVehicleDocumentListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidVehicleFK { get; set; }
    public string VehicleAllFKPlateNumber { get; set; }
    public Guid GidDocumentType { get; set; }
    public string DocumentTypeFKName { get; set; }
    public string DocumentName { get; set; }
    public DateTime DocumentDate { get; set; }
    public DateTime DocumentLastDate { get; set; }
    public string? DocumentFile { get; set; }
    public string? Description { get; set; }
}