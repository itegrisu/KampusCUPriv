using Core.Application.Responses;

namespace Application.Features.AuthManagementFeatures.AuthRolePages.Queries.GetByGid;

public class GetByGidAuthRolePageResponse : IResponse
{
    public Guid Gid { get; set; } 
    public string AuthRoleFKRoleName { get; set; }
    public string AuthPageFKPageName { get; set; }
    public string AuthPageFKRedirectName { get; set; }
    public int RowNo { get; set; }
}