using Application.Features.AuthManagementFeatures.AuthRolePages.Queries.GetByGid;
using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.AuthManagementFeatures.AuthRolePages.Commands.Update;

public class UpdatedAuthRolePageResponse : BaseResponse, IResponse
{
    public GetByGidAuthRolePageResponse Obj { get; set; }
}