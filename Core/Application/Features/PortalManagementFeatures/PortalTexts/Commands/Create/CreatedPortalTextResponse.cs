using Application.Features.Base;
using Application.Features.PortalManagementFeatures.PortalTexts.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.PortalManagementFeatures.PortalTexts.Commands.Create;

public class CreatedPortalTextResponse : BaseResponse, IResponse
{
    public GetByGidPortalTextResponse Obj { get; set; }
}