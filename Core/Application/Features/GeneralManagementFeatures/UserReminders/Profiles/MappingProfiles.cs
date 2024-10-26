using Application.Features.GeneralManagementFeatures.UserReminders.Commands.Create;
using Application.Features.GeneralManagementFeatures.UserReminders.Commands.Delete;
using Application.Features.GeneralManagementFeatures.UserReminders.Commands.Update;
using Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetByUserGid;
using Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.UserReminder, CreateUserReminderCommand>().ReverseMap();
        CreateMap<X.UserReminder, CreatedUserReminderResponse>().ReverseMap();
        CreateMap<X.UserReminder, UpdateUserReminderCommand>().ReverseMap();
        CreateMap<X.UserReminder, UpdatedUserReminderResponse>().ReverseMap();
        CreateMap<X.UserReminder, DeleteUserReminderCommand>().ReverseMap();
        CreateMap<X.UserReminder, DeletedUserReminderResponse>().ReverseMap();

		CreateMap<X.UserReminder, GetByGidUserReminderResponse>().ReverseMap();

        CreateMap<X.UserReminder, GetListUserReminderListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.UserReminder>, GetListResponse<GetListUserReminderListItemDto>>().ReverseMap();


        CreateMap<X.UserReminder, GetByUserGidListUserReminderListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.UserReminder>, GetListResponse<GetByUserGidListUserReminderListItemDto>>().ReverseMap();
    }
}