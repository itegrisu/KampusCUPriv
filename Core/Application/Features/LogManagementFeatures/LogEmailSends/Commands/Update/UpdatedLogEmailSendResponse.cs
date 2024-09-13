using Application.Features.Base;
using Application.Features.LogManagementFeatures.LogEmailSends.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.LogManagementFeatures.LogEmailSends.Commands.Update;

public class UpdatedLogEmailSendResponse : BaseResponse, IResponse
{
    public GetByGidLogEmailSendResponse Obj { get; set; }
}