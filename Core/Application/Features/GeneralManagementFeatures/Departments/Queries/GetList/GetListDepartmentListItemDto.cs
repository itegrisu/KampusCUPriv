using Core.Application.Dtos;

namespace Application.Features.GeneralManagementFeatures.Departments.Queries.GetList;

public class GetListDepartmentListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidMainAdminFK { get; set; }
    public string MainAdminFKFullName { get; set; }
    public Guid? GidCoAdminFK { get; set; }
    public string CoAdminFKFullName { get; set; }
    public string Name { get; set; }
    public string? Detail { get; set; }


}