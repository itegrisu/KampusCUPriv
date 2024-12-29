using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Commands.Update;

public class UpdatedReservationHotelPartTimeWorkerResponse : BaseResponse, IResponse
{
    public GetByGidReservationHotelPartTimeWorkerResponse Obj { get; set; }
}