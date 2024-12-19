using Application.Features.NotificationManagementFeatures.Notifications.Commands.Create;
using Application.Features.NotificationManagementFeatures.Notifications.Commands.Delete;
using Application.Features.NotificationManagementFeatures.Notifications.Commands.Update;
using Application.Features.NotificationManagementFeatures.Notifications.Queries.GetByGid;
using Application.Features.NotificationManagementFeatures.Notifications.Queries.GetByUserGid;
using Application.Features.NotificationManagementFeatures.Notifications.Queries.GetList;
using Application.Features.NotificationManagementFeatures.Notifications.Queries.GetUnreadByUserGid;
using Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetByUserGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.NotificationManagements;

namespace Application.Features.NotificationManagementFeatures.Notifications.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Notification, CreateNotificationCommand>().ReverseMap();
        CreateMap<Notification, CreatedNotificationResponse>().ReverseMap();
        CreateMap<Notification, UpdateNotificationCommand>().ReverseMap();
        CreateMap<Notification, UpdatedNotificationResponse>().ReverseMap();
        CreateMap<Notification, DeleteNotificationCommand>().ReverseMap();
        CreateMap<Notification, DeletedNotificationResponse>().ReverseMap();
        CreateMap<Notification, GetByGidNotificationResponse>().ReverseMap();
        CreateMap<Notification, GetListNotificationListItemDto>().ReverseMap();
        CreateMap<IPaginate<Notification>, GetListResponse<GetListNotificationListItemDto>>().ReverseMap();
        CreateMap<Notification, GetByUserGidNotificationListItemDto>().ReverseMap();
        CreateMap<IPaginate<Notification>, GetListResponse<GetByUserGidNotificationListItemDto>>().ReverseMap();
        
        CreateMap<Notification, GetUnreadByUserGidListItemDto>().ReverseMap();
        CreateMap<IPaginate<Notification>, GetListResponse<GetUnreadByUserGidListItemDto>>().ReverseMap();

    }
}