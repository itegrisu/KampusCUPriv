using Application.Features.Base;
using Application.Features.NotificationManagementFeatures.Notifications.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.NotificationManagementFeatures.Notifications.Commands.MarkTheReadByGid;

public class MarkTheReadByGidResponse  : BaseResponse, IResponse
{
    public GetByGidNotificationResponse? Obj { get; set; }
}