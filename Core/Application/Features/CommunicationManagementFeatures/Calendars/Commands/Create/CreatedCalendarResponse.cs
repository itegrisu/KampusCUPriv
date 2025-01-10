using Application.Features.Base;
using Application.Features.CommunicationFeatures.Calendars.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.CommunicationFeatures.Calendars.Commands.Create;

public class CreatedCalendarResponse : BaseResponse, IResponse
{
    public GetByGidCalendarResponse Obj { get; set; }
}