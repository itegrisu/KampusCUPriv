using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Commands.Create;

public class CreatedReservationHotelPartTimeWorkerResponse : BaseResponse, IResponse
{
    public GetByGidReservationHotelPartTimeWorkerResponse Obj { get; set; }
}