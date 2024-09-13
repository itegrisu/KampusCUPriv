using Application.Features.Base;
using Application.Features.PortalManagementFeatures.PortalTexts.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.PortalManagementFeatures.PortalTexts.Commands.Update;

public class UpdatedPortalTextResponse : BaseResponse, IResponse
{
    public GetByGidPortalTextResponse Obj { get; set; }
}