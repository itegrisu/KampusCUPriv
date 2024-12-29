using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Commands.Create;
using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Commands.Update;
using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodations.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.GuestAccommodation, CreateGuestAccommodationCommand>().ReverseMap();
        CreateMap<X.GuestAccommodation, CreatedGuestAccommodationResponse>().ReverseMap();
        CreateMap<X.GuestAccommodation, UpdateGuestAccommodationCommand>().ReverseMap();
        CreateMap<X.GuestAccommodation, UpdatedGuestAccommodationResponse>().ReverseMap();
        CreateMap<X.GuestAccommodation, DeleteGuestAccommodationCommand>().ReverseMap();
        CreateMap<X.GuestAccommodation, DeletedGuestAccommodationResponse>().ReverseMap();

		CreateMap<X.GuestAccommodation, GetByGidGuestAccommodationResponse>().ReverseMap();

        CreateMap<X.GuestAccommodation, GetListGuestAccommodationListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.GuestAccommodation>, GetListResponse<GetListGuestAccommodationListItemDto>>().ReverseMap();
    }
}