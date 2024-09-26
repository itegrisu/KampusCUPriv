using Application.Features.Base;
using Application.Features.SupportManagementFeatures.SupportRequests.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.SupportManagementFeatures.SupportRequests.Commands.Create;

public class CreatedSupportRequestResponse : BaseResponse, IResponse
{
    public GetByGidSupportRequestResponse Obj { get; set; }
}