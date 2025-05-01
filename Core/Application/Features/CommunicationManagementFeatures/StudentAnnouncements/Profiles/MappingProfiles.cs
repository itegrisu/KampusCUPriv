using Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Create;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Delete;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Update;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Queries.GetByGid;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Queries.GetList;
using Application.Features.CommunicationManagementFeatures.StudentAnnouncements.Commands.MarkAllAsRead;
using Application.Features.CommunicationManagementFeatures.StudentAnnouncements.Queries.GetByUserGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.CommunicationManagements;

namespace Application.Features.CommunicationFeatures.StudentAnnouncements.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.StudentAnnouncement, CreateStudentAnnouncementCommand>().ReverseMap();
        CreateMap<X.StudentAnnouncement, CreatedStudentAnnouncementResponse>().ReverseMap();
        CreateMap<X.StudentAnnouncement, UpdateStudentAnnouncementCommand>().ReverseMap();
        CreateMap<X.StudentAnnouncement, UpdatedStudentAnnouncementResponse>().ReverseMap();
        CreateMap<X.StudentAnnouncement, DeleteStudentAnnouncementCommand>().ReverseMap();
        CreateMap<X.StudentAnnouncement, DeletedStudentAnnouncementResponse>().ReverseMap();

		CreateMap<X.StudentAnnouncement, GetByGidStudentAnnouncementResponse>().ReverseMap();

        CreateMap<X.StudentAnnouncement, GetListStudentAnnouncementListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.StudentAnnouncement>, GetListResponse<GetListStudentAnnouncementListItemDto>>().ReverseMap();

        CreateMap<X.StudentAnnouncement, GetByUserGidListStudentAnnouncementListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.StudentAnnouncement>, GetListResponse<GetByUserGidListStudentAnnouncementListItemDto>>().ReverseMap();

        CreateMap<X.StudentAnnouncement, MarkAllAsReadStudentAnnouncementCommand>().ReverseMap();
        CreateMap<X.StudentAnnouncement, MarkAllAsReadStudentAnnouncementResponse>().ReverseMap();
    }
}