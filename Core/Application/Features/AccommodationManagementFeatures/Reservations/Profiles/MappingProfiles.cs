using Application.Features.AccommodationManagementFeatures.Reservations.Commands.Create;
using Application.Features.AccommodationManagementFeatures.Reservations.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.Reservations.Commands.Update;
using Application.Features.AccommodationManagementFeatures.Reservations.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.Reservations.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.Reservations.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.Reservation, CreateReservationCommand>().ReverseMap();
        CreateMap<X.Reservation, CreatedReservationResponse>().ReverseMap();
        CreateMap<X.Reservation, UpdateReservationCommand>().ReverseMap();
        CreateMap<X.Reservation, UpdatedReservationResponse>().ReverseMap();
        CreateMap<X.Reservation, DeleteReservationCommand>().ReverseMap();
        CreateMap<X.Reservation, DeletedReservationResponse>().ReverseMap();

		CreateMap<X.Reservation, GetByGidReservationResponse>().ReverseMap();

        CreateMap<X.Reservation, GetListReservationListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Reservation>, GetListResponse<GetListReservationListItemDto>>().ReverseMap();
    }
}