using Application.Features.AccommodationManagementFeatures.ReservationRooms.Commands.Create;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Commands.Update;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.ReservationRoom, CreateReservationRoomCommand>().ReverseMap();
        CreateMap<X.ReservationRoom, CreatedReservationRoomResponse>().ReverseMap();
        CreateMap<X.ReservationRoom, UpdateReservationRoomCommand>().ReverseMap();
        CreateMap<X.ReservationRoom, UpdatedReservationRoomResponse>().ReverseMap();
        CreateMap<X.ReservationRoom, DeleteReservationRoomCommand>().ReverseMap();
        CreateMap<X.ReservationRoom, DeletedReservationRoomResponse>().ReverseMap();

		CreateMap<X.ReservationRoom, GetByGidReservationRoomResponse>().ReverseMap();

        CreateMap<X.ReservationRoom, GetListReservationRoomListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.ReservationRoom>, GetListResponse<GetListReservationRoomListItemDto>>().ReverseMap();
    }
}