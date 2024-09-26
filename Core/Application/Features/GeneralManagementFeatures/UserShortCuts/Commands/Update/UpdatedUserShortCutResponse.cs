using Application.Features.Base;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.Update;

public class UpdatedUserShortCutResponse : BaseResponse, IResponse
{
    public GetByGidUserShortCutResponse Obj { get; set; }
}