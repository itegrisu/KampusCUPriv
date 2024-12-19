using Application.Features.Base;
using Application.Features.NotificationManagementFeatures.Notifications.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.NotificationManagementFeatures.Notifications.Commands.Update;

public class UpdatedNotificationResponse : BaseResponse, IResponse
{
    public GetByGidNotificationResponse? Obj { get; set; }
}