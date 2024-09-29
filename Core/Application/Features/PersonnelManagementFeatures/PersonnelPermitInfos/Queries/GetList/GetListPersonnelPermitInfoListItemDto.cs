using Core.Application.Dtos;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetList;

public class GetListPersonnelPermitInfoListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidPersonnelFK { get; set; }
    public string UserFKFullName { get; set; }
    public Guid GidPermitFK { get; set; }
    public string PermitTypeFKName { get; set; }

    public DateTime PermitStartDate { get; set; }
    public DateTime PermitEndDate { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }


}