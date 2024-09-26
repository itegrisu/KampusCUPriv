using Application.Features.Base;
using Application.Features.SupportManagementFeatures.SupportMessages.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Commands.Update;

public class UpdatedSupportMessageResponse : BaseResponse, IResponse
{
    public GetByGidSupportMessageResponse Obj { get; set; }
}