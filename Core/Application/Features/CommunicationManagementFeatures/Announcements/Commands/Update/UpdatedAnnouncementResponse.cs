using Application.Features.Base;
using Application.Features.CommunicationFeatures.Announcements.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.CommunicationFeatures.Announcements.Commands.Update;

public class UpdatedAnnouncementResponse : BaseResponse, IResponse
{
    public GetByGidAnnouncementResponse Obj { get; set; }
}