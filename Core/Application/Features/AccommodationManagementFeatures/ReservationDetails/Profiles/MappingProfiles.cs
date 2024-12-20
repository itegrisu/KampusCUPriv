using Application.Features.AccommodationManagementFeatures.ReservationDetails.Commands.Create;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Commands.Update;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.ReservationDetail, CreateReservationDetailCommand>().ReverseMap();
        CreateMap<X.ReservationDetail, CreatedReservationDetailResponse>().ReverseMap();
        CreateMap<X.ReservationDetail, UpdateReservationDetailCommand>().ReverseMap();
        CreateMap<X.ReservationDetail, UpdatedReservationDetailResponse>().ReverseMap();
        CreateMap<X.ReservationDetail, DeleteReservationDetailCommand>().ReverseMap();
        CreateMap<X.ReservationDetail, DeletedReservationDetailResponse>().ReverseMap();

		CreateMap<X.ReservationDetail, GetByGidReservationDetailResponse>().ReverseMap();

        CreateMap<X.ReservationDetail, GetListReservationDetailListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.ReservationDetail>, GetListResponse<GetListReservationDetailListItemDto>>().ReverseMap();
    }
}