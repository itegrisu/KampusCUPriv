using Application.Features.Base;
using Application.Features.CommunicationFeatures.Events.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.CommunicationFeatures.Events.Commands.Create;

public class CreatedEventResponse : BaseResponse, IResponse
{
    public GetByGidEventResponse Obj { get; set; }
}