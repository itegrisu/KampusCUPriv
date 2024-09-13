using Core.Application.Responses;
using Core.Enum;

namespace Application.Features.AuthManagementFeatures.AuthRoles.Queries.GetByGid;

public class GetByGidAuthRoleResponse : IResponse
{
    public Guid Gid { get; set; }
    public string RoleName { get; set; }
    public string? RoleDescription { get; set; }
    public string? IconImage { get; set; }
    public int RowNo { get; set; }
    public DataState DataState { get; set; }
}