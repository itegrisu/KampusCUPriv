using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Queries.GetByGid;
using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Commands.Update;

public class UpdatedAnnouncementRecipientResponse : BaseResponse, IResponse
{
    public GetByGidAnnouncementRecipientResponse Obj { get; set; }
}