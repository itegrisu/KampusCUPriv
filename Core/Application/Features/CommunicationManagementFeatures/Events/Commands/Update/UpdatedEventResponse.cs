using Application.Features.Base;
using Application.Features.CommunicationFeatures.Events.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.CommunicationFeatures.Events.Commands.Update;

public class UpdatedEventResponse : BaseResponse, IResponse
{
    public GetByGidEventResponse Obj { get; set; }
}