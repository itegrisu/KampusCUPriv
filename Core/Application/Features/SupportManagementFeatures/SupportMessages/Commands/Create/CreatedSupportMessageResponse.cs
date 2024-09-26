using Application.Features.Base;
using Application.Features.SupportManagementFeatures.SupportMessages.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Commands.Create;

public class CreatedSupportMessageResponse : BaseResponse, IResponse
{
    public GetByGidSupportMessageResponse Obj { get; set; }
}