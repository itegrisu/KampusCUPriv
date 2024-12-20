using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Commands.Create;
using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Commands.Update;
using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.ReservationHotelStaff, CreateReservationHotelStaffCommand>().ReverseMap();
        CreateMap<X.ReservationHotelStaff, CreatedReservationHotelStaffResponse>().ReverseMap();
        CreateMap<X.ReservationHotelStaff, UpdateReservationHotelStaffCommand>().ReverseMap();
        CreateMap<X.ReservationHotelStaff, UpdatedReservationHotelStaffResponse>().ReverseMap();
        CreateMap<X.ReservationHotelStaff, DeleteReservationHotelStaffCommand>().ReverseMap();
        CreateMap<X.ReservationHotelStaff, DeletedReservationHotelStaffResponse>().ReverseMap();

		CreateMap<X.ReservationHotelStaff, GetByGidReservationHotelStaffResponse>().ReverseMap();

        CreateMap<X.ReservationHotelStaff, GetListReservationHotelStaffListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.ReservationHotelStaff>, GetListResponse<GetListReservationHotelStaffListItemDto>>().ReverseMap();
    }
}