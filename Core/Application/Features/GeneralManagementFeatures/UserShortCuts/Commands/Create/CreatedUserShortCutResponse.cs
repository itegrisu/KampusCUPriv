using Application.Features.Base;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.Create;

public class CreatedUserShortCutResponse : BaseResponse, IResponse
{
    public GetByGidUserShortCutResponse Obj { get; set; }
}