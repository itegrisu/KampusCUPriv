using Application.Features.AuthManagementFeatures.AuthRoles.Queries.GetByGid;
using Application.Features.Base;
using Core.Application.Responses;


namespace Application.Features.AuthManagementFeatures.AuthRoles.Commands.Create;

public class CreatedAuthRoleResponse : BaseResponse, IResponse
{
    public GetByGidAuthRoleResponse Obj { get; set; }
}