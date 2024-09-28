using Application.Features.Base;
using Application.Features.GeneralManagementFeatures.UserModuleAuths.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.UserModuleAuths.Commands.Update;

public class UpdatedUserModuleAuthResponse : BaseResponse, IResponse
{
    public GetByGidUserModuleAuthResponse Obj { get; set; }
}