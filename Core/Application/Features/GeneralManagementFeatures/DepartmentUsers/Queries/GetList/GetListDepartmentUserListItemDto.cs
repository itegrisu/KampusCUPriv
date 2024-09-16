using Core.Application.Dtos;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetList;

public class GetListDepartmentUserListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidDepartmanFK { get; set; }
    public string DepartmentFKDepartmanAdi { get; set; }
    public Guid GidPersonelFK { get; set; }
    public string UserFKTamAd { get; set; }

}