using Core.Application.Dtos;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetList;

public class GetListPersonnelPassportInfoListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidPersonnelFK { get; set; }
    public string UserFKFullName { get; set; }
    public string PassportNo { get; set; }
    public DateTime DateOfIssue { get; set; }
    public DateTime ValidityDate { get; set; }
    public string? Document { get; set; }

}