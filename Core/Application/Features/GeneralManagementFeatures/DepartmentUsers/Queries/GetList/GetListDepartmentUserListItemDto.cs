using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetList;

public class GetListDepartmentUserListItemDto : IDto
{
    public Guid Gid { get; set; }
public Guid GidDepartmanFK { get; set; }
public Department DepartmentFK { get; set; }
public Guid GidPersonelFK { get; set; }
public User UserFK { get; set; }



}