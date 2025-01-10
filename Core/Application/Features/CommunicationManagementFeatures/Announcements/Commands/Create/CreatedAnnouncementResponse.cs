using Application.Features.Base;
using Application.Features.CommunicationFeatures.Announcements.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.CommunicationFeatures.Announcements.Commands.Create;

public class CreatedAnnouncementResponse : BaseResponse, IResponse
{
    public GetByGidAnnouncementResponse Obj { get; set; }
}