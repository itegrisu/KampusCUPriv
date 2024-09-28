using Application.Features.Base;
using Application.Features.GeneralManagementFeatures.UserModuleAuths.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.GeneralManagementFeatures.UserModuleAuths.Commands.Create;

public class CreatedUserModuleAuthResponse : BaseResponse, IResponse
{
    public GetByGidUserModuleAuthResponse Obj { get; set; }
}