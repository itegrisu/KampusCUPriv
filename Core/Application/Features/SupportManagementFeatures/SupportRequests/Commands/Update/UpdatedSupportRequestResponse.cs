using Application.Features.Base;
using Application.Features.SupportManagementFeatures.SupportRequests.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.SupportManagementFeatures.SupportRequests.Commands.Update;

public class UpdatedSupportRequestResponse : BaseResponse, IResponse
{
    public GetByGidSupportRequestResponse Obj { get; set; }
}