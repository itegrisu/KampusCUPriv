using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Commands.Create;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Commands.Update;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetByGuestGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.GuestAccommodationRoom, CreateGuestAccommodationRoomCommand>().ReverseMap();
        CreateMap<X.GuestAccommodationRoom, CreatedGuestAccommodationRoomResponse>().ReverseMap();
        CreateMap<X.GuestAccommodationRoom, UpdateGuestAccommodationRoomCommand>().ReverseMap();
        CreateMap<X.GuestAccommodationRoom, UpdatedGuestAccommodationRoomResponse>().ReverseMap();
        CreateMap<X.GuestAccommodationRoom, DeleteGuestAccommodationRoomCommand>().ReverseMap();
        CreateMap<X.GuestAccommodationRoom, DeletedGuestAccommodationRoomResponse>().ReverseMap();

		CreateMap<X.GuestAccommodationRoom, GetByGidGuestAccommodationRoomResponse>().ReverseMap();

        CreateMap<X.GuestAccommodationRoom, GetListGuestAccommodationRoomListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.GuestAccommodationRoom>, GetListResponse<GetListGuestAccommodationRoomListItemDto>>().ReverseMap();

        CreateMap<X.GuestAccommodationRoom, GetByGuestGidListGuestAccommodationRoomListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.GuestAccommodationRoom>, GetListResponse<GetByGuestGidListGuestAccommodationRoomListItemDto>>().ReverseMap();
    }
}