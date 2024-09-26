using Application.Features.AnnouncementManagementFeatures.Announcements.Queries.GetByGid;
using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Commands.Update;

public class UpdatedAnnouncementResponse : BaseResponse, IResponse
{
    public GetByGidAnnouncementResponse Obj { get; set; }
}