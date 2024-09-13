using Application.Features.Base;
using Application.Features.PortalManagementFeatures.PortalParameters.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.PortalManagementFeatures.PortalParameters.Commands.Update;

public class UpdatedPortalParameterResponse : BaseResponse, IResponse
{
    public GetByGidPortalParameterResponse Obj { get; set; }
}