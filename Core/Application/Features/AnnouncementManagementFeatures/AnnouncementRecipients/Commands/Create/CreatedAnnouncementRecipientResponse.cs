using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Queries.GetByGid;
using Application.Features.AnnouncementManagementFeatures.Announcements.Queries.GetByGid;
using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Commands.Create;

public class CreatedAnnouncementRecipientResponse : BaseResponse, IResponse
{
    public GetByGidAnnouncementRecipientResponse Obj { get; set; }
}