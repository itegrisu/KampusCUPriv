using Application.Features.Base;
using Application.Features.PortalManagementFeatures.PortalParameters.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.PortalManagementFeatures.PortalParameters.Commands.Create;

public class CreatedPortalParameterResponse : BaseResponse, IResponse
{
    public GetByGidPortalParameterResponse Obj { get; set; }
}