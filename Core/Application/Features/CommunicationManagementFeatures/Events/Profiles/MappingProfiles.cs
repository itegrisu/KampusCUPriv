using Application.Features.CommunicationFeatures.Events.Commands.Create;
using Application.Features.CommunicationFeatures.Events.Commands.Delete;
using Application.Features.CommunicationFeatures.Events.Commands.Update;
using Application.Features.CommunicationFeatures.Events.Queries.GetByGid;
using Application.Features.CommunicationFeatures.Events.Queries.GetList;
using Application.Features.CommunicationManagementFeatures.Events.Queries.GetByClubGid;
using Application.Features.CommunicationManagementFeatures.Events.Queries.GetByCount;
using Application.Features.CommunicationManagementFeatures.Events.Queries.GetByUserGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.CommunicationManagements;

namespace Application.Features.CommunicationFeatures.Events.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.Event, CreateEventCommand>().ReverseMap();
        CreateMap<X.Event, CreatedEventResponse>().ReverseMap();
        CreateMap<X.Event, UpdateEventCommand>().ReverseMap();
        CreateMap<X.Event, UpdatedEventResponse>().ReverseMap();
        CreateMap<X.Event, DeleteEventCommand>().ReverseMap();
        CreateMap<X.Event, DeletedEventResponse>().ReverseMap();

		CreateMap<X.Event, GetByGidEventResponse>().ReverseMap();

        CreateMap<X.Event, GetListEventListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Event>, GetListResponse<GetListEventListItemDto>>().ReverseMap();

        CreateMap<X.Event, GetByUserGidListEventListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Event>, GetListResponse<GetByUserGidListEventListItemDto>>().ReverseMap();

        CreateMap<X.Event, GetByCountListEventListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Event>, GetListResponse<GetByCountListEventListItemDto>>().ReverseMap();

        CreateMap<X.Event, GetByClubGidListEventListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Event>, GetListResponse<GetByClubGidListEventListItemDto>>().ReverseMap();
    }
}