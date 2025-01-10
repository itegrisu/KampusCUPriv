using Application.Features.Base;
using Application.Features.CommunicationFeatures.Calendars.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.CommunicationFeatures.Calendars.Commands.Update;

public class UpdatedCalendarResponse : BaseResponse, IResponse
{
    public GetByGidCalendarResponse Obj { get; set; }
}