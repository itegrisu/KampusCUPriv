using Application.Features.Base;
using Application.Features.LogManagementFeatures.LogEmailSends.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.LogManagementFeatures.LogEmailSends.Commands.Create;

public class CreatedLogEmailSendResponse : BaseResponse, IResponse
{
    public GetByGidLogEmailSendResponse Obj { get; set; }
}