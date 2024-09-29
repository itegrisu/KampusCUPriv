using Core.Application.Dtos;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetList;

public class GetListDepartmentUserListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidDepartmentFK { get; set; }
    public string DepartmentFKName { get; set; }
    public Guid GidPersonnelFK { get; set; }
    public string UserFKFullName { get; set; }

}