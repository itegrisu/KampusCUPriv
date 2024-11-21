using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.TransportationManagementFeatures.TransportationPersonnels.Queries.GetList;

public class GetListTransportationPersonnelListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid? GidTransportationServiceFK { get; set; }
    public string TransportationServiceFKServiceNo { get; set; }
    public Guid GidStaffPersonnelFK { get; set; }
    public string UserFKFullName { get; set; }
    public EnumStaffType StaffType { get; set; }
    public string? Description { get; set; }
}