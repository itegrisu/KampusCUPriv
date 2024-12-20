using Application.Features.AccommodationManagementFeatures.Guests.Commands.Create;
using Application.Features.AccommodationManagementFeatures.Guests.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.Guests.Commands.Update;
using Application.Features.AccommodationManagementFeatures.Guests.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.Guests.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.Guests.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.Guest, CreateGuestCommand>().ReverseMap();
        CreateMap<X.Guest, CreatedGuestResponse>().ReverseMap();
        CreateMap<X.Guest, UpdateGuestCommand>().ReverseMap();
        CreateMap<X.Guest, UpdatedGuestResponse>().ReverseMap();
        CreateMap<X.Guest, DeleteGuestCommand>().ReverseMap();
        CreateMap<X.Guest, DeletedGuestResponse>().ReverseMap();

		CreateMap<X.Guest, GetByGidGuestResponse>().ReverseMap();

        CreateMap<X.Guest, GetListGuestListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Guest>, GetListResponse<GetListGuestListItemDto>>().ReverseMap();
    }
}