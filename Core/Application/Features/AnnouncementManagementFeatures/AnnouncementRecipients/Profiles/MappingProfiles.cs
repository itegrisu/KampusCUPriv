using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Commands.Create;
using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Commands.Delete;
using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Commands.Update;
using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Queries.GetByGid;
using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AnnouncementManagements;

namespace Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AnnouncementRecipient, CreateAnnouncementRecipientCommand>().ReverseMap();
        CreateMap<AnnouncementRecipient, CreatedAnnouncementRecipientResponse>().ReverseMap();
        CreateMap<AnnouncementRecipient, UpdateAnnouncementRecipientCommand>().ReverseMap();
        CreateMap<AnnouncementRecipient, UpdatedAnnouncementRecipientResponse>().ReverseMap();
        CreateMap<AnnouncementRecipient, DeleteAnnouncementRecipientCommand>().ReverseMap();
        CreateMap<AnnouncementRecipient, DeletedAnnouncementRecipientResponse>().ReverseMap();
        CreateMap<AnnouncementRecipient, GetByGidAnnouncementRecipientResponse>().ReverseMap();
        CreateMap<AnnouncementRecipient, GetListAnnouncementRecipientListItemDto>().ReverseMap();
        CreateMap<IPaginate<AnnouncementRecipient>, GetListResponse<GetListAnnouncementRecipientListItemDto>>().ReverseMap();
    }
}