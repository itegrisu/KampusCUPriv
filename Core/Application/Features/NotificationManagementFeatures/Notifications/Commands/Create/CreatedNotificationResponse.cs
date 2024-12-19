using Application.Features.Base;
using Application.Features.NotificationManagementFeatures.Notifications.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.NotificationManagementFeatures.Notifications.Commands.Create;

public class CreatedNotificationResponse : BaseResponse, IResponse
{
    public GetByGidNotificationResponse? Obj { get; set; }
}