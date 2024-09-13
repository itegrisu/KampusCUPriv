using Core.Application.Responses;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetByGid;

public class GetByGidAuthUserRoleResponse : IResponse
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public Guid GidRoleFK { get; set; }
    public Guid GidPageFK { get; set; }
    public int RowNo { get; set; }
    public string UserFKFullName { get; set; }
    public string? AuthRoleFKRoleName { get; set; }
    public string? AuthPageFKPageName { get; set; }
}