using Application.Features.Base;
using Application.Features.NotificationManagementFeatures.Notifications.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.NotificationManagementFeatures.Notifications.Commands.MarkTheReadAllNotification;

public class MarkTheReadAllNotificationResponse : BaseResponse, IResponse
{
    public GetByGidNotificationResponse? Obj { get; set; }
}