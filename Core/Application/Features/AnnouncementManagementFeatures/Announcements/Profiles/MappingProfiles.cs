using Application.Features.AnnouncementManagementFeatures.Announcements.Commands.Create;
using Application.Features.AnnouncementManagementFeatures.Announcements.Commands.Delete;
using Application.Features.AnnouncementManagementFeatures.Announcements.Commands.Update;
using Application.Features.AnnouncementManagementFeatures.Announcements.Queries.GetByGid;
using Application.Features.AnnouncementManagementFeatures.Announcements.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AnnouncementManagements;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Announcement, CreateAnnouncementCommand>().ReverseMap();
        CreateMap<Announcement, CreatedAnnouncementResponse>().ReverseMap();
        CreateMap<Announcement, UpdateAnnouncementCommand>().ReverseMap();
        CreateMap<Announcement, UpdatedAnnouncementResponse>().ReverseMap();
        CreateMap<Announcement, DeleteAnnouncementCommand>().ReverseMap();
        CreateMap<Announcement, DeletedAnnouncementResponse>().ReverseMap();
        CreateMap<Announcement, GetByGidAnnouncementResponse>().ReverseMap();
        CreateMap<Announcement, GetListAnnouncementListItemDto>().ReverseMap();
        CreateMap<IPaginate<Announcement>, GetListResponse<GetListAnnouncementListItemDto>>().ReverseMap();
    }
}