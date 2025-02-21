using Application.Features.CommunicationFeatures.Announcements.Commands.Create;
using Application.Features.CommunicationFeatures.Announcements.Commands.Delete;
using Application.Features.CommunicationFeatures.Announcements.Commands.Update;
using Application.Features.CommunicationFeatures.Announcements.Queries.GetByGid;
using Application.Features.CommunicationFeatures.Announcements.Queries.GetList;
using Application.Features.CommunicationManagementFeatures.Announcements.Queries.GetByClubGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.CommunicationManagements;

namespace Application.Features.CommunicationFeatures.Announcements.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.Announcement, CreateAnnouncementCommand>().ReverseMap();
        CreateMap<X.Announcement, CreatedAnnouncementResponse>().ReverseMap();
        CreateMap<X.Announcement, UpdateAnnouncementCommand>().ReverseMap();
        CreateMap<X.Announcement, UpdatedAnnouncementResponse>().ReverseMap();
        CreateMap<X.Announcement, DeleteAnnouncementCommand>().ReverseMap();
        CreateMap<X.Announcement, DeletedAnnouncementResponse>().ReverseMap();

		CreateMap<X.Announcement, GetByGidAnnouncementResponse>().ReverseMap();

        CreateMap<X.Announcement, GetListAnnouncementListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Announcement>, GetListResponse<GetListAnnouncementListItemDto>>().ReverseMap();

        CreateMap<X.Announcement, GetByClubGidListAnnouncementListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Announcement>, GetListResponse<GetByClubGidListAnnouncementListItemDto>>().ReverseMap();
    }
}