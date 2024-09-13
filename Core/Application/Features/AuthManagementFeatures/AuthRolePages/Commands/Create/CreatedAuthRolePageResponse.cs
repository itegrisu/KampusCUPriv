using Application.Features.AuthManagementFeatures.AuthRolePages.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthRoles.Queries.GetByGid;
using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.AuthManagementFeatures.AuthRolePages.Commands.Create;

public class CreatedAuthRolePageResponse : BaseResponse, IResponse
{
    public GetByGidAuthRolePageResponse Obj { get; set; }
}