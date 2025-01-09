using Application.Features.AccommodationManagementFeatures.ReservationHotels.Commands.Create;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Commands.Update;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetList;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetListByPartTime;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetListByWorker;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.ReservationHotel, CreateReservationHotelCommand>().ReverseMap();
        CreateMap<X.ReservationHotel, CreatedReservationHotelResponse>().ReverseMap();
        CreateMap<X.ReservationHotel, UpdateReservationHotelCommand>().ReverseMap();
        CreateMap<X.ReservationHotel, UpdatedReservationHotelResponse>().ReverseMap();
        CreateMap<X.ReservationHotel, DeleteReservationHotelCommand>().ReverseMap();
        CreateMap<X.ReservationHotel, DeletedReservationHotelResponse>().ReverseMap();

        CreateMap<X.ReservationHotel, GetByGidReservationHotelResponse>().ReverseMap();

        CreateMap<X.ReservationHotel, GetListReservationHotelListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.ReservationHotel>, GetListResponse<GetListReservationHotelListItemDto>>().ReverseMap();

        CreateMap<X.ReservationHotel, GetListByPartTimeReservationHotelListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.ReservationHotel>, GetListResponse<GetListByPartTimeReservationHotelListItemDto>>().ReverseMap();

        CreateMap<X.ReservationHotel, GetListByWorkerReservationHotelListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.ReservationHotel>, GetListResponse<GetListByWorkerReservationHotelListItemDto>>().ReverseMap();
    }
}