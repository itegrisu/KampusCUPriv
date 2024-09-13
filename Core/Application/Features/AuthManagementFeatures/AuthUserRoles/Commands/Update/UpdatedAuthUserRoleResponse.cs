using Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetByGid;
using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.Update;

public class UpdatedAuthUserRoleResponse : BaseResponse, IResponse
{
    public GetByGidAuthUserRoleResponse Obj { get; set; }
}