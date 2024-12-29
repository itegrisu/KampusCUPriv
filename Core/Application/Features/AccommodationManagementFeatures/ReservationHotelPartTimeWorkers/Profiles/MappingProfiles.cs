using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Commands.Create;
using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Commands.Update;
using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.ReservationHotelPartTimeWorker, CreateReservationHotelPartTimeWorkerCommand>().ReverseMap();
        CreateMap<X.ReservationHotelPartTimeWorker, CreatedReservationHotelPartTimeWorkerResponse>().ReverseMap();
        CreateMap<X.ReservationHotelPartTimeWorker, UpdateReservationHotelPartTimeWorkerCommand>().ReverseMap();
        CreateMap<X.ReservationHotelPartTimeWorker, UpdatedReservationHotelPartTimeWorkerResponse>().ReverseMap();
        CreateMap<X.ReservationHotelPartTimeWorker, DeleteReservationHotelPartTimeWorkerCommand>().ReverseMap();
        CreateMap<X.ReservationHotelPartTimeWorker, DeletedReservationHotelPartTimeWorkerResponse>().ReverseMap();

		CreateMap<X.ReservationHotelPartTimeWorker, GetByGidReservationHotelPartTimeWorkerResponse>().ReverseMap();

        CreateMap<X.ReservationHotelPartTimeWorker, GetListReservationHotelPartTimeWorkerListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.ReservationHotelPartTimeWorker>, GetListResponse<GetListReservationHotelPartTimeWorkerListItemDto>>().ReverseMap();
    }
}