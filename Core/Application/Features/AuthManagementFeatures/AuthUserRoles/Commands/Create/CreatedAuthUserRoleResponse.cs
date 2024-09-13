using Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetByGid;
using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.Create;

public class CreatedAuthUserRoleResponse : BaseResponse, IResponse
{
    public GetByGidAuthUserRoleResponse Obj { get; set; }
}