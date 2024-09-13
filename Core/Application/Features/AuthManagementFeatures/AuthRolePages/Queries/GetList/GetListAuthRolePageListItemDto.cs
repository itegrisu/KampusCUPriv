using Core.Application.Dtos;

namespace Application.Features.AuthManagementFeatures.AuthRolePages.Queries.GetList;

public class GetListAuthRolePageListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string AuthRoleFKRoleName { get; set; }
    public string AuthPageFKPageName { get; set; }
    public string AuthPageFKRedirectName { get; set; }
    public string AuthPageFKPhysicalFilePath { get; set; }
    public int RowNo { get; set; }

}