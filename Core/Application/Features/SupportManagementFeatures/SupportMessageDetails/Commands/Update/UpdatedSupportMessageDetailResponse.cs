using Application.Features.Base;
using Application.Features.SupportManagementFeatures.SupportMessageDetails.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.SupportManagementFeatures.SupportMessageDetails.Commands.Update;

public class UpdatedSupportMessageDetailResponse : BaseResponse, IResponse
{
    public GetByGidSupportMessageDetailResponse Obj { get; set; }
}