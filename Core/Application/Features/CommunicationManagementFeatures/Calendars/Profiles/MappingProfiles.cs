using Application.Features.CommunicationFeatures.Calendars.Commands.Create;
using Application.Features.CommunicationFeatures.Calendars.Commands.Delete;
using Application.Features.CommunicationFeatures.Calendars.Commands.Update;
using Application.Features.CommunicationFeatures.Calendars.Queries.GetByGid;
using Application.Features.CommunicationFeatures.Calendars.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.CommunicationManagements;

namespace Application.Features.CommunicationFeatures.Calendars.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.Calendar, CreateCalendarCommand>().ReverseMap();
        CreateMap<X.Calendar, CreatedCalendarResponse>().ReverseMap();
        CreateMap<X.Calendar, UpdateCalendarCommand>().ReverseMap();
        CreateMap<X.Calendar, UpdatedCalendarResponse>().ReverseMap();
        CreateMap<X.Calendar, DeleteCalendarCommand>().ReverseMap();
        CreateMap<X.Calendar, DeletedCalendarResponse>().ReverseMap();

		CreateMap<X.Calendar, GetByGidCalendarResponse>().ReverseMap();

        CreateMap<X.Calendar, GetListCalendarListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Calendar>, GetListResponse<GetListCalendarListItemDto>>().ReverseMap();
    }
}